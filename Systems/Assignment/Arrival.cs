using UnityEngine;
using System;


//自定义VGF.Assignment库，添加处理玩家到达某个地点的功能
namespace VGF.Assignment
{
    //在Unity编辑器中创建该类的实例
    [CreateAssetMenu(menuName = "Visual Game Framework/Assignment/Arrival", order = 0)]
    public class Arrival : Assignment, IDisposable 
    {
        private ArrivalPlugin arrivalPlugin;

        //可以实现IDisposable接口，判断玩家是否到达某地
        public static Arrival CreateInstance(string nameOfColliderGameObject, string name,string description,bool display = false)
        {
            Debug.Log(nameOfColliderGameObject);
            Arrival arrival = ScriptableObject.CreateInstance<Arrival>();
            Collider2D collider = GameObject.Find(nameOfColliderGameObject).GetComponent<Collider2D>();
            Debug.Log(collider);
            if (!collider)
                throw new System.Exception($"没有找到{nameOfColliderGameObject}");
            collider.isTrigger = true;
            ArrivalPlugin arrivalPlugin = collider.gameObject.AddComponent<ArrivalPlugin>();
            arrivalPlugin.arrival = arrival;
            arrival.arrivalPlugin = arrivalPlugin;
            arrival.Name = name;
            arrival.Description = description;
            arrival.Display = display;
            return arrival;
        }

        //置反
        public override bool Check()
        {
            return false;
        }

        //清除ArrivalPlugin组件，在不需要的时候释放资源
        public void Dispose()
        {
            Destroy(arrivalPlugin);
        }

        //完成任务后，清除ArrivalPlugin组件，释放资源
        public override void Finish()
        {
            this.Dispose();
        }
    }
}
