using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//该类用于控制角色行为
public class PlayerControl : MonoBehaviour, IInputProvider
{
    public event Action OnJump;

    private void Update()
    {
        //读取玩家的按键输入
        inputState.movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        //按下“W、A、S、D”按键时触发相应的操作
        if (Input.GetKeyDown(KeyCode.W))
        {
            //
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //
        }
    }

    //获取输入状态
    InputState inputState;
    public InputState GetState()
    {
        return inputState;
    }
}
