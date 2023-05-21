using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该类用于控制角色移动
[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    private Rigidbody2D rb;     //角色控制
    private float gravityValue = -9.81f;        //模拟重力
    public IInputProvider[] providers;          //接收不同的输入
    public Animator animator;
    public float Speed;

    //初始化controller和providers
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        providers = GetComponents<IInputProvider>();
        animator = GetComponent<Animator>();
    }

    //逐帧处理输入和控制角色移动
    void Update()
    {
        Process();
    }
    private void Process()
    {
        float InputX = 0;
        float InputY = 0;
        foreach (IInputProvider provider in providers)
        {
            InputState inputState = provider.GetState();
            if (inputState.cover)
            {
                InputX = inputState.movement.x;
                //处理输入和控制角色移动
                InputY = inputState.movement.y;
                //控制角色的移动
            }
        }
        if (Mathf.Abs(InputX) > 0.01f || Mathf.Abs(InputY) > 0.01f)
        {
            animator.SetFloat("InputX", InputX);
            animator.SetFloat("InputY", InputY);
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
        rb.velocity = new Vector2(InputX,InputY) * Speed;
    }
}

//描述当前输入状态
public interface IInputProvider
{
    //public event Action OnJump;
    public InputState GetState();
}

//描述角色的运动状态
public struct InputState
{
    public Vector2 movement;
    public bool cover;
}
