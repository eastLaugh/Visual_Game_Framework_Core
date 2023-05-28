using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AutumnFramework;
using UnityEngine.UI;
using System;
using System.Reflection;
using System.Linq;

//它包含一系列用于创建、呈现和操作富文本（包括超链接、颜色、字体等）的类和方法。
using TMPro;


//自定义WordZone库，包含了动画与UI、FSM、渲染层的功能
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

        //初始化动画和UI的变量
        private void Awake()
        {

            this.Bean();
            sequence = DOTween.Sequence()
                .Append(rectTransform.DOAnchorPosY(0, 0.2f).From(new Vector2(0, -100f)))
                .Join(WordCanvasGroup.DOFade(0f, 0f))
                .Join(WordCanvasGroup.DOFade(1, 0.2f))
                .SetAutoKill(false);
        }


        private void OnEnable()
        {
            EventHandler.PlayerDie += OnPlayerDie;

        }
        private void OnDisable()
        {
            EventHandler.PlayerDie -= OnPlayerDie;

        }
        private void OnPlayerDie()
        {
            pieces.Clear();
        }

        //添加动画效果
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
        //表示状态机中可能的状态
        public enum EState
        {
            HideAndReady/*Start*/, Rendering, EscapeRendering, WaitUser, Timeline
        };
        private void OnDestroy()
        {
            this.UnBean();
        }
        //表示状态机本身
        private static FSM<EState> fsm = new();
        private Queue<WordPiece> pieces = new();
        public EState? State => fsm.CurrentState;

        private WordPiece currentPiece;

        private Coroutine coroutine;
        void FSM()
        {
            fsm.State(EState.HideAndReady).OnEnter(() =>            //状态的进入
            {
                sequence.SmoothRewind();
                sequence.OnStepComplete(() =>
                {
                    WordCanvasGroup.gameObject.SetActive(false);
                });
            }).OnUpdate(() =>                                       //状态的转换
            {
                if (pieces.Count > 0)
                {
                    fsm.ChangeState(EState.Rendering);
                }
            }).OnExit(() =>                                         //状态的退出
            {
                sequence.OnStepComplete(null);
                WordCanvasGroup.gameObject.SetActive(true);
            });

            //状态的进入
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
                Text.text = "";
                StartCoroutine(RenderPiece(currentPiece, true));
            });
            fsm.State(EState.WaitUser).OnEnter(() =>
            {
                sequence.Complete();
            }).OnExit(() =>
            {
                currentPiece.OnFinish?.Invoke();
            });

            fsm.ChangeState(EState.HideAndReady);
        }
        #endregion


        #region 渲染层
        //该协程函数用于处理传入的WordPiece类型的数据
        IEnumerator RenderPiece(WordPiece piece, bool instant = false)
        {
            if (fsm.CurrentState == EState.Rendering || fsm.CurrentState == EState.EscapeRendering)
            {
                Commands.reset();
                Tone.Instant = fsm.CurrentState == EState.EscapeRendering;

                //对不同文本的渲染
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

        //该协程函数用于实现打字机效果
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
                //打字的声音
                SoundManager.Instance.PlaySound(Globals.Type);
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

        //将文本解析成单词，并将其存储到队列中
        public void ParseAndEnque(string unparsedText, Action callback = null)
        {
            WordPiece wordPiece = WordPiece.Parser(unparsedText);
            wordPiece.OnFinish = callback;
            pieces.Enqueue(wordPiece);
        }

        //点击区域
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

        //状态切换
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

            ///////////////TODO!!!!!!!!!!!!!!!!!!!!!!!!!
            if (VGF_Player_2D.Instance != null)
            {
                if (State == EState.HideAndReady)
                    VGF_Player_2D.Instance.Mute = false;
                else
                    VGF_Player_2D.Instance.Mute = true;
            }

        }

        //使用GUI绘制用户界面，其中包括一个文本区域和一个按钮，并且显示当前状态
        string textAreaText;
        private void OnGUI()
        {
            // textAreaText = GUILayout.TextArea(textAreaText, new GUIStyle(GUI.skin.textArea) { fontSize = 32 });
            // if (GUILayout.Button("Pop Word Zone", new GUIStyle(GUI.skin.button) { fontSize = 32 }))
            // {
            //     pieces.Enqueue(WordPiece.Parser(textAreaText));
            // }
            // GUILayout.Label(fsm.CurrentState.ToString(), new GUIStyle(GUI.skin.label) { fontSize = 32 });
        }
    }
}
