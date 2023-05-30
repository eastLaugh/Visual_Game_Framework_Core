using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using AutumnFramework;

//引用VGF的核心库，实现框架
using VGF.Assignment;
using VGF.UI;
using VGF.SceneSystem;
using VGF.Inventory;


//自定义VGF.Plot库，完善章节内的运行逻辑
namespace VGF.Plot
{
    /***********************************基础逻辑************************************/
    [Beans]
    public abstract class SessionBase : MonoBehaviour
    {
        private void Start()
        {
            this.Bean();
        }

        [Autowired]
        private static AudioCenter audioCenter;

        private bool isPlaying;

        //章节是否正在运行
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
        #region 1.字幕系统（Caption）
        /// <summary>
        /// 显示黑色文本
        /// </summary>
        /// <param name="content"></param>
        public void Caption(string content, float seconds = 1f, Action callback = null)
        {
            //显示指定内容的字幕（可以设置内容、时长、回调函数）
            CaptionLoader.instance.Push(content, seconds, callback);
        }

        [Obsolete]
        public void InstantCaption(string content, float seconds = 1f, Action callback = null)
        {
            //停止之前的字幕
            CaptionLoader.instance.Stop();
            //显示新的字幕（可以设置内容、时长、回调函数）
            Caption(content, seconds, callback);
        }

        //立刻清空当前正在显示的字幕
        public void CapitionEmpty()
        {
            CaptionLoader.instance.Stop();
        }
        #endregion

        #region 2.场景系统
        [SerializeField]
        private SceneLoader sceneLoader;

        //切换到指定的场景
        public void SceneMove(string name)
        {
            sceneLoader.SwitchSceneByName(name);
        }

        //切换场景后执行的行为逻辑
        protected void SceneMoveThen(string name, Action action)
        {
            sceneLoader.AfterSceneLoaded = action;
            SceneMove(name);
        }

        //绑定指定场景加载完后的事件，即绑定切换场景后执行的行为逻辑
        public void BindSceneEvent(string name, Action<SceneLoader.Msg> action)
        {

                sceneLoader.BindSceneEvent(name, action);


        }

        //方便调用前一个函数
        public void Scene(string name, Action<SceneLoader.Msg> action)
        {
            BindSceneEvent(name, action);
        }
        #endregion

        #region 3.NPC及对话系统
        //操控游戏角色
        protected struct CharacterChainOperator
        {
            public string name { get; private set; }
            public GameObject gameObject { get; private set; }

            //角色信息的加载
            Character character;
            public CharacterChainOperator(string name)
            {
                this.name = name;
                gameObject = GameObject.Find(name);
                Debug.Log(gameObject);
                character = gameObject.GetComponent<Character>() ?? gameObject.AddComponent<Character>();

                tmps = new();
            }

            public List<(string, Action)> tmps;
            //给角色添加互动组件并注册一个动作
            public CharacterChainOperator Interactive(Action action, bool isDefault = false)
            {
                Interactive interactive = gameObject.GetComponent<Interactive>() ?? gameObject.AddComponent<Interactive>();
                if (!isDefault)
                    interactive.RegisterAction(action);
                else
                    interactive.RegisterDefaultAction(action);
                return this;
            }

            //使角色在游戏中说话
            public CharacterChainOperator Say(string text)
            {
                Say say = gameObject.GetComponent<Say>() ?? gameObject.AddComponent<Say>();
                say.SayMsg(text);
                return this;
            }

            [Obsolete]  //操纵游戏角色
            public CharacterChainOperator Pool(Action action, float rate)
            {
                Pool pool = gameObject.GetComponent<Pool>() ?? gameObject.AddComponent<Pool>();
                return this;
            }


            public CharacterChainOperator Opt(string text, Action action = null)
            {
                tmps.Add((text, action));
                ApplyOpt();
                return this;
            }
            public CharacterChainOperator Opt()
            {
                tmps.Clear();
                ApplyOpt();
                return this;
            }
            
            void ApplyOpt(){
                string[] texts = this.tmps.Select(s=>s.Item1).ToArray();
                var tmpstmp = this.tmps;
                OptZone.Show(gameObject.transform.position,texts,i=>{
                    tmpstmp[i].Item2?.Invoke();
                });
            }

        }

        //创建一个角色对象，方便角色操作
        protected CharacterChainOperator at(string name)
        {
            return new CharacterChainOperator(name);
        }

        [Autowired] //向游戏对话框中添加文字
        private static WordZone.WordZone wordZone;
        public static void Word(string text,string name = null, Action callback=null)
        {
            wordZone.ShowName(name);
            wordZone.ParseAndEnque(text,callback);
        }
        #endregion

        #region 4.存档系统
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

        #region 5.背包系统
        /// <summary>
        /// 查找背包中的物品
        /// </summary>
        public bool SearchBagItem(int ID, int num)
        {
            return InventoryManager.Instance.SearchItem(ID, num);
        }
        #endregion

        #region 6.时间线系统（Timeline）
        //触发一个指定名称的时间线的播放
        protected void Timeline(string name)
        {
            EventHandler.PlayTimelineInvoke(name);

        }
        protected void PlayTimeline(string name)
        {
            EventHandler.PlayTimelineInvoke(name);
        }

        //在播放指定名称的时间线的同时，执行指定委托对象的方法
        protected void Timeline(string name, Action action)
        {
            if (name == string.Empty)
                action?.Invoke();
            else
                EventHandler.PlayTimelineInvoke(name, action);
        }
        #endregion

        #region 7.音乐系统
        protected void PlayMusic(string name)
        {
            audioCenter.Play(name);
        }
        #endregion

        #region 8.技能系统
        protected void SetSkillAvaliable(string name, bool isAvaliable)
        {
            SkillSystem.SetSkillAvailable(name, isAvaliable);
        }
        #endregion

        #region 9.任务系统
        protected void Arrival(string name, Action<AssignmentFinishMsg> action,string nameOfAssignment,string description,bool display = false)
        {
            Arrival arrival = Assignment.Arrival.CreateInstance(name,nameOfAssignment,description,display);
            arrival.OnAssignmentFinished += action;
            arrival.Bean();
        }

        protected void AssignItem(int ItemID, int itemAmount, bool isTaken, Action<AssignmentFinishMsg> finEvent,string name,string description)
        {
            InventoryAssignment inventoryAssignment = InventoryAssignment.CreatAssignment(ItemID, itemAmount, isTaken,name,description);
            inventoryAssignment.OnAssignmentFinished += finEvent;
            inventoryAssignment.Bean();
        }
        #endregion

        /********************************其他公共函数*********************************/
        //1.等待一定时间后执行指定的回调函数
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

        //2.分配任务和监视任务的进度


        //3.把角色移动到指定的游戏对象位置（传送）
        protected void MoveTo(string GameObjectName)
        {
            VGF_Player_2D.Instance.transform.position = GameObject.Find(GameObjectName).transform.position;
        }




        protected void Opt()
        {

        }
    }
}
