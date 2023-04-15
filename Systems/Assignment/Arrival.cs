
using UnityEngine;
using System;

namespace VGF.Assignment
{


    [CreateAssetMenu(menuName = "Visual Game Framework/Assignment/Arrival", order = 0)]
    public class Arrival : Assignment , IDisposable
    {
        private ArrivalPlugin arrivalPlugin;


        new public static Arrival CreateInstance(string nameOfColliderGameObject)
        {
            Arrival arrival = ScriptableObject.CreateInstance<Arrival>();
            Collider collider = GameObject.Find(nameOfColliderGameObject).GetComponent<Collider>();
            if (!collider)
                throw new System.Exception($"没有找到{nameOfColliderGameObject}");
            collider.isTrigger = true;
            ArrivalPlugin arrivalPlugin = collider.gameObject.AddComponent<ArrivalPlugin>();
            arrivalPlugin.arrival = arrival;

            arrival.arrivalPlugin = arrivalPlugin;
            return arrival;
        }

        public override bool Check()
        {
            return false;
        }

        public void Dispose()
        {
            Destroy(arrivalPlugin);
        }

        public override void Finish()
        {
            this.Dispose();
        }
    }
}