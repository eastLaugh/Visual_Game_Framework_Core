using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该类用于控制角色移动
[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    private CharacterController controller;     //角色控制
    private float gravityValue = -9.81f;        //模拟重力
    public IInputProvider[] providers;          //接收不同的输入

    //初始化controller和providers
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        providers = GetComponents<IInputProvider>();
    }

    //逐帧处理输入和控制角色移动
    void Update()
    {
        Process();
    }
    private void Process()
    {
        foreach (IInputProvider provider in providers)
        {
            //处理输入和控制角色移动
            InputState inputState = provider.GetState();

            //控制角色的移动
            controller.Move(inputState.movement * Time.deltaTime * Settings.PlayerSpeed);
            controller.Move(new Vector3(0, gravityValue * Time.deltaTime, 0));
        }
    }
}

//描述当前输入状态
public interface IInputProvider
{
    public event Action OnJump;
    public InputState GetState();
}

//描述角色的运动状态
public struct InputState
{
    public Vector3 movement;
}
