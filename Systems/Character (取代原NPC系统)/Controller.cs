using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������ڿ��ƽ�ɫ�ƶ�
public class Controller : MonoBehaviour
{
    private CharacterController controller;     //��ɫ����
    private float gravityValue = -9.81f;        //ģ������
    public IInputProvider[] providers;          //���ղ�ͬ������

    //��ʼ��controller��providers
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        providers = GetComponents<IInputProvider>();
    }

    //��֡��������Ϳ��ƽ�ɫ�ƶ�
    void Update()
    {
        Process();
    }
    private void Process()
    {
        foreach (IInputProvider provider in providers)
        {
            //��������Ϳ��ƽ�ɫ�ƶ�
            InputState inputState = provider.GetState();

            //���ƽ�ɫ���ƶ�
            controller.Move(inputState.movement * Time.deltaTime * Settings.PlayerSpeed);
            controller.Move(new Vector3(0, gravityValue * Time.deltaTime, 0));
        }
    }
}

//������ǰ����״̬
public interface IInputProvider
{
    public event Action OnJump;
    public InputState GetState();
}

//������ɫ���˶�״̬
public struct InputState
{
    public Vector3 movement;
}
