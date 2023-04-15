using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using AutumnFramework;

using VGF.Assignment;
using VGF.UI;
using VGF.SceneSystem;

using VGF.Inventory;

namespace VGF.Plot
{

    [Beans]
    public abstract class ChapterBase : MonoBehaviour
    {


        private void Start()
        {
            this.Bean();
        }

        [Autowired]
        private static AudioCenter audioCenter;

        private bool isPlaying;
        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
        }
        //[SerializeField]
        //private Camera playerCamera;
        /// <summary>
        /// 游戏剧情入口
        /// </summary>
        public abstract void Run();
        /// <summary>
        /// 切换下一章节
        /// </summary>
        public void VGF()
        {
            Debug.LogWarning("<color=orange>切换到下一章</color>");
            PlotManager.Instance.NextChapter();
        }

        /*********************************各系统函数*********************************/



        #region Caption系统
        /// <summary>
        /// 显示黑色文本
        /// </summary>
        /// <param name="content"></param>
        public void Caption(string content, float seconds = 1f, Action callback = null)
        {
            CaptionLoader.instance.Push(content, seconds, callback);
        }

        [Obsolete]
        public void InstantCaption(string content, float seconds = 1f, Action callback = null)
        {
            CaptionLoader.instance.Stop();

            Caption(content, seconds, callback);
        }

        public void CapitionEmpty()
        {
            CaptionLoader.instance.Stop();
        }
        #endregion

        #region 场景系统


        [Autowired]
        private SceneLoader sceneLoader;
        public void SceneMove(string name)
        {
            sceneLoader.SwitchSceneByName(name);
        }

        protected void SceneMoveThen(string name, Action action)
        {
            sceneLoader.AfterSceneLoaded = action;
            SceneMove(name);
        }
        public void BindSceneEvent(string name, Action<SceneLoader.Msg> action)
        {
            sceneLoader.BindSceneEvent(name, action);
        }

        public void Scene(string name, Action<SceneLoader.Msg> action)
        {
            BindSceneEvent(name, action);
        }
        #endregion

        #region NPC及对话系统


        [Autowired]
        private static WordZone.WordZone wordZone;
        public static void Word(string text){
            wordZone.ParseAndEnque(text);
        }

        #region 存档系统
        /// <summary>
        /// 切换章节自动存档（要在chapters里手动调用）
        /// </summary>

        public void AutoSave()
        {
            GlobalSystem.SaveGame();
            HintLoader.Instance.HintWithSeconds("AutoSave Completed", 1);
            Debug.Log("AutoSave Completed");
        }
        #endregion

        #region 背包系统
        /// <summary>
        /// 查找背包中的物品
        /// </summary>
        public bool SearchBagItem(int ID, int num)
        {
            return InventoryManager.Instance.SearchItem(ID, num);
        }
        #endregion

        #region Timeline系统
        protected void Timeline(string name)
        {
            EventHandler.PlayTimelineInvoke(name);

        }
        protected void PlayTimeline(string name)
        {
            EventHandler.PlayTimelineInvoke(name);
        }
        ///
        protected void Timeline(string name, Action action)
        {
            if (name == string.Empty)
                action?.Invoke();
            else
                EventHandler.PlayTimelineInvoke(name, action);

        }
        #endregion

        #region 音乐系统
        protected void PlayMusic(string name)
        {
            audioCenter.Play(name);
        }
        #endregion



        /*********************************其他公共函数*********************************/
        /// <summary>
        /// 等待一段时间并执行
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Coroutine WaitThen(float seconds, Action action)
        {
            return StartCoroutine(IEnumeratorWaitThen(seconds, action));
        }

        IEnumerator IEnumeratorWaitThen(float seconds, Action action)
        {
            yield return new WaitForSecondsRealtime(seconds);
            action?.Invoke();
        }



        protected void Arrival(string name, Action<AssignmentFinishMsg> action)
        {
            Arrival arrival = Assignment.Arrival.CreateInstance(name);
            arrival.OnAssignmentFinished += action;
            arrival.Bean();
        }


        protected void MoveTo(string GameObjectName)
        {
            Player.instance.transform.position = GameObject.Find(GameObjectName).transform.position;
        }
    }

    #endregion
}