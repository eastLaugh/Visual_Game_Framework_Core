using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using AutumnFramework;
using UnityEngine.UI;
using System;
using System.Reflection;
using System.Linq;

namespace WordZone
{
    [Beans]
    public class WordZone : MonoBehaviour
    {

        #region 动画与UI
        public TMP_Text Text;
        public CanvasGroup WordCanvasGroup;
        public RectTransform rectTransform;
        public Button button;
        private Sequence sequence;

        private void Awake()
        {
            this.Bean();
            sequence = DOTween.Sequence()
                .Append(rectTransform.DOAnchorPosY(0, 0.2f).From(new Vector2(0, -100f)))
                .Join(WordCanvasGroup.DOFade(0f, 0f))
                .Join(WordCanvasGroup.DOFade(1, 0.2f))
                .SetAutoKill(false);
        }
        void Anime(bool enable)
        {
            if (enable)
            {
                sequence.Restart();

            }
            else
            {
                sequence.SmoothRewind();
            }
        }
        #endregion

        #region FSM
        public enum EState
        {
            HideAndReady/*Start*/, Rendering, EscapeRendering, WaitUser, Timeline
        };
        private static FSM<EState> fsm = new();
        private Queue<WordPiece> pieces = new();
        public EState? State => fsm.CurrentState;

        private WordPiece currentPiece;

        private Coroutine coroutine;
        void FSM()
        {
            fsm.State(EState.HideAndReady).OnEnter(() =>
            {
                sequence.SmoothRewind();
                sequence.OnStepComplete(() =>
                {
                    WordCanvasGroup.gameObject.SetActive(false);
                });
            }).OnUpdate(() =>
            {
                if (pieces.Count > 0)
                {
                    fsm.ChangeState(EState.Rendering);
                }
            }).OnExit(() =>
            {
                sequence.OnStepComplete(null);
                WordCanvasGroup.gameObject.SetActive(true);
            });

            fsm.State(EState.Rendering).OnEnter(() =>
            {
                sequence.Restart();

                currentPiece = pieces.Dequeue();
                Text.text = "";
                coroutine = StartCoroutine(RenderPiece(currentPiece));
            });

            fsm.State(EState.EscapeRendering).OnEnter(() =>
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                StartCoroutine(RenderPiece(currentPiece, true));
            });
            fsm.State(EState.WaitUser).OnEnter(() =>
            {
                sequence.Complete();
            });

            fsm.ChangeState(EState.HideAndReady);
        }

        #endregion


        #region 渲染层
        IEnumerator RenderPiece(WordPiece piece, bool instant = false)
        {
            if (fsm.CurrentState == EState.Rendering || fsm.CurrentState == EState.EscapeRendering)
            {
                Commands.reset();
                Tone.Instant = fsm.CurrentState == EState.EscapeRendering;
                foreach (WordPiece.Token token in piece.tokens)
                {
                    switch (token.tokenType)
                    {
                        case WordPiece.Token.TokenType.Word:
                            yield return Type(Tone.Runtime.v, Text, token.take);
                            break;
                        case WordPiece.Token.TokenType.Command:
                            object returnValue = null;
                            try
                            {
                                returnValue = typeof(Commands).GetMethod(token.take, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, token.paraments.Cast<object>().ToArray());

                            }
                            catch (Exception e)
                            {
                                Debug.LogErrorFormat("指令{0}执行失败，参数列表：{1}", token.take, "( " + string.Join(" , ", token.paraments) + " )");
                                Debug.LogWarningFormat("期望的函数信息：{0}", typeof(Commands).GetMethod(token.take));
                                Debug.LogError(e);
                            }
                            if (returnValue is IEnumerator enumerator)
                                yield return enumerator;
                            break;
                        case WordPiece.Token.TokenType.EmptyBracket:
                            Commands.reset();
                            break;
                    }
                }
            }

            while (!sequence.IsComplete() && !instant)
            {
                yield return null;
            };

            coroutine = null;
            fsm.ChangeState(EState.WaitUser);
        }

        IEnumerator Type(int v, TMPro.TMP_Text text, string take)
        {
            if (v.Equals(int.MaxValue))
            {
                text.text += take;
                yield break;
            }

            bool inXMLElement = false;

            // 1秒v个字     1/v秒1个字
            for (int i = 0; i < take.Length; i++)
            {

                switch (take[i])
                {
                    case '<':
                        inXMLElement = true;
                        break;

                    case '>':
                        inXMLElement = false;
                        break;
                }
                text.text += take[i];
                if (!inXMLElement)
                {
                    yield return Commands.halt(1f / (float)v);
                }
            }
        }
        #endregion
        private void Start()
        {
            FSM();
        }
        public void ParseAndEnque(string unparsedText){
            pieces.Enqueue(WordPiece.Parser(unparsedText));
        }
        public void OnClickZone()
        {
            if (fsm.CurrentState == EState.WaitUser)
            {
                Next();
            }
            else if (fsm.CurrentState == EState.Rendering)
            {
                SkipRender();
            }

        }

        private void SkipRender()
        {
            if (fsm.CurrentState == EState.Rendering)
            {
                fsm.ChangeState(EState.EscapeRendering);
            }
        }

        public void Next()
        {
            if (fsm.CurrentState == EState.WaitUser)
            {
                fsm.ChangeState(EState.HideAndReady);
            }
        }

        private void Update()
        {
            fsm.update();
        }
        string textAreaText;
        private void OnGUI()
        {
            textAreaText = GUILayout.TextArea(textAreaText, new GUIStyle(GUI.skin.textArea) { fontSize = 32 });
            if (GUILayout.Button("Pop Word Zone", new GUIStyle(GUI.skin.button) { fontSize = 32 }))
            {
                pieces.Enqueue(WordPiece.Parser(textAreaText));
            }
            GUILayout.Label(fsm.CurrentState.ToString(), new GUIStyle(GUI.skin.label) { fontSize = 32 });
        }



    }
}