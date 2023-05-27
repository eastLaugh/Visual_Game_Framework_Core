using UnityEngine;
using System;
using AutumnFramework;
using VGF.Plot;


//自定义VGF.Assignment，增加任务系统的相关功能
namespace VGF.Assignment
{
    //标记为可以被注入（Autowired）的组件或服务
    [Beans]
    [System.Serializable]
    public abstract class Assignment : ScriptableObject
    {
        [Autowired]
        public static Assignment[] assignments;
        public abstract bool Check();
        public bool Stationary = false;         //表示玩家是否在特定位置停留
        public bool Preferential { get; set; }  //表示是否是优先任务
        public string Name;
        public string Description;
        //任务完成时触发
        public Action<AssignmentFinishMsg> OnAssignmentFinished;
        //用于完成任务
        public abstract void Finish();

        //将当前任务标记为已完成，并且触发任务完成的事件
        public void Ticked()
        {
            
            Finish();
            OnAssignmentFinished?.Invoke(new AssignmentFinishMsg());
            this.UnBean();
        }

        //逐帧执行
        private void Update()
        {
            //Debug.Log("目前assignment长度" + assignments.Length);
            //检测任务是否完成
            if (Check())
            {
                Ticked();
            }
        }
    }

    //任务完成时的提示信息
    public struct AssignmentFinishMsg
    {
        //可个性化开发
    }

    //提供外部程序读取和修改的接口
    public interface IHaveNameLore
    {
        public string Name { get; set; }
        public string Lore { get; set; }
    }
}
