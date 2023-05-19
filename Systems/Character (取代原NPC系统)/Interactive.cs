using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using System;
using UnityEngine.Events;
using Cinemachine;          //相机功能库
using VGF.UI;


//确保该脚本所挂载的游戏对象有 Character 和 Collider 组件
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Collider2D))]
//该类拓展了角色与游戏对象的交互功能
public class Interactive : MonoBehaviour, ICharacter
{

    // [Autowired]     //该成员将由IoC容器自动注入一个CameraControl类型的对象
    // public static CameraControl cameraControl;

    private ICinemachineCamera currentCamera => FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;

    private Queue<UnityAction> Actions = new();
    private UnityAction defaultAction;

    private bool @in;

    //当角色进入与Interactive相关的碰撞器时，会将相机的跟踪目标设为当前Interactive对象
    public void OnPlayerEnter()
    {
        HintLoader.Instance.HintOn("按E互动");
        @in = true;
        try
        {
            currentCamera.LookAt = transform;
        }
        catch 
        {

        }
    }

    private void Update()
    {
       
        //按下"E"键时触发
        if (@in && Input.GetKeyDown(KeyCode.E))
        {
            HintLoader.Instance.HintOff();

            //
            GetComponent<Character>().InteractAllCom();
        }
        else
        {

        }
    }


    //当角色退出与Interactive相关的碰撞器时，会将相机的跟踪目标设回玩家
    public void OnPlayerExit()
    {
        @in = false;
        HintLoader.Instance.HintOff();
        try
        {
            currentCamera.LookAt = Character.Player.transform;
        }
        catch 
        {
            
        }
    }

    //注册事件的处理程序
    internal void RegisterAction(Action action)
    {
        Actions.Enqueue(new UnityAction(action));
    }
    internal void RegisterDefaultAction(Action action)
    {
        defaultAction = new UnityAction(action);
    }

    //尝试取出队列中的元素并执行该元素所代表的操作
    public void OnInteract()
    {
        if (Actions.TryDequeue(out UnityAction result))
        {
            result.Invoke();
        }
        else
        {
            defaultAction?.Invoke();
        }
    }
}
