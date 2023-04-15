
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


#region 状态机
// using System;
// using System.Collections.Generic;

// internal class State
//     {
//         public Action OnUpdate;
//         public Action OnEnable;
//         public Action Ondisable;

//         public void init()
//         {

//         }

//     }
// internal class FSM<T> where T : Enum
// {

//     private Dictionary<T, State> dictStates = new();
//     public FSM()
//     {
//         foreach (var value in Enum.GetValues(typeof(T)) as T[])
//         {
//             dictStates.Add(value, new State());
//         }
//     }

//     public State State(T t){
//         if(dictStates.TryGetValue(t,out var value)){
//             return value;
//         }else{
//             throw new Exception("找不到状态");
//         }
//     }

// }



public class CustomState 
{
    private Action mOnEnter;
    private Action mOnUpdate;
    private Action mOnFixedUpdate;
    private Action mOnExit;
    public CustomState OnEnter(Action act)
    {
        mOnEnter = act;
        return this;
    }
    public CustomState OnUpdate(Action act)
    {
        mOnUpdate = act;
        return this;
    }
    public CustomState OnFixedUpdate(Action act)
    {
        mOnFixedUpdate = act;
        return this;
    }
    public CustomState OnExit(Action act)
    {
        mOnExit = act;
        return this;
    }

    public void Enter()
    {
        mOnEnter?.Invoke();
    }

    public void Update()
    {
        mOnUpdate?.Invoke();
    }


    public void FixedUpdate()
    {
        mOnFixedUpdate?.Invoke();
    }


    public void Exit()
    {
        mOnExit?.Invoke();
    }
}

public class FSM<T> where T:struct,Enum
{
    public Dictionary<T, CustomState> States = new();

    public CustomState State(T t)
    {
        if (!States.ContainsKey(t))
        {
            States.Add(t, new CustomState());
        }
        return States[t];
    }

    private CustomState CurrentCustomState;
    private T? currentState=null;

    public T? CurrentState { get => currentState;}

    public void ChangeState(T t)
    {
        if(currentState.HasValue && currentState.GetValueOrDefault().Equals(t)){
            Debug.LogWarning("FSM已经处于状态"+t+",自切换会重新执行一遍OnEnter");
        }
        if (States.TryGetValue(t, out var s))
        {
            CurrentCustomState?.Exit();
            currentState=t;
            CurrentCustomState = s;
            CurrentCustomState.Enter();
        }
        else
        {
            throw new ApplicationException("未注册状态，请使用State注册状态");
        }
    }
    public void FixedUpdate()
    {
        CurrentCustomState?.FixedUpdate();
    }

    public void update()
    {
        CurrentCustomState?.Update();
    }

    public void Reset()
    {
        currentState= null;
        CurrentCustomState =null;
        States.Clear();
    }
}


#endregion

#region 敌人相关

public class Round
{
    public string[] gossips{get;set;}

    public void Options(string rewrite,Action action)
    {
        
    }
}


#endregion
