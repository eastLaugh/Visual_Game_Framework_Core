using UnityEngine;
using VGF.Plot;
using System;
using AutumnFramework;

namespace VGF.Assignment
{
    [Beans]
    public abstract class Assignment : ScriptableObject
    {

        [Autowired]
        private Assignment[] assignments ;
        public abstract bool Check();
        public bool Stationary = false;
        public bool Preferential { get; set; }//TODO

        public Action<AssignmentFinishMsg> OnAssignmentFinished;
        public abstract void Finish();

        public void Ticked()
        {
            this.UnBean();
            Finish();
            OnAssignmentFinished?.Invoke(new AssignmentFinishMsg());
        }

        private void Update() {
            if(Check()){
                Ticked();
            }
        }

    }

    public struct AssignmentFinishMsg
    {

    }

    public interface IHaveNameLore{
        public string Name{get;set;}
        public string Lore{get;set;}

    }
}