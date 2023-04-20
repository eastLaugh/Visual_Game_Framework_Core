using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using System;
using UnityEngine.Events;
using Cinemachine;          //������ܿ�


//ȷ���ýű������ص���Ϸ������ Character �� Collider ���
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Collider))]
//������չ�˽�ɫ����Ϸ����Ľ�������
public class Interactive : MonoBehaviour, ICharacter
{

    [Autowired]     //�ó�Ա����IoC�����Զ�ע��һ��CameraControl���͵Ķ���
    public static CameraControl cameraControl;

    private ICinemachineCamera currentCamera => FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;

    private UnityAction OnInteractive;

    private bool @in;

    //����ɫ������Interactive��ص���ײ��ʱ���Ὣ����ĸ���Ŀ����Ϊ��ǰInteractive����
    public void OnPlayerEnter()
    {
        @in = true;
        currentCamera.LookAt = transform;
    }

    private void Update()
    {
        //����"E"��ʱ����
        if (@in && Input.GetKeyDown(KeyCode.E))
        {
            OnInteractive?.Invoke();
        }
    }

    //����ɫ�˳���Interactive��ص���ײ��ʱ���Ὣ����ĸ���Ŀ��������
    public void OnPlayerExit()
    {
        @in = false;
        currentCamera.LookAt = Character.Player.transform;
    }

    //ע���¼��Ĵ������
    internal void RegisterAction(Action action)
    {
        OnInteractive += new UnityAction(action);
    }
}
