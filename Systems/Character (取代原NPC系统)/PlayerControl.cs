using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�������ڿ��ƽ�ɫ��Ϊ
public class PlayerControl : MonoBehaviour, IInputProvider
{
    public event Action OnJump;

    private void Update()
    {
        //��ȡ��ҵİ�������
        inputState.movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        //���¡�W��A��S��D������ʱ������Ӧ�Ĳ���
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

    //��ȡ����״̬
    InputState inputState;
    public InputState GetState()
    {
        return inputState;
    }
}
