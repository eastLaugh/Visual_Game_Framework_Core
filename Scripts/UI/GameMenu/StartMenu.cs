using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //���㴦��������صĲ��������ء�ж�ء��������л������


//�Զ���VGF.SceneSystem���������ó����Ķ��ֹ���
namespace VGF.SceneSystem
{
    //�̳���MonoBehaviour���࣬�������
    public class StartMenu : MonoBehaviour
    {
        public GameObject InfoCanvas;       //�洢һ����Ϸ����Canvas��
        public GameObject SettingCanvas;    //�洢�������Canvas��Ϸ����
        private Canvas MainCanvas;          //�洢��ǰ�ű������ص���Ϸ����� Canvas ���
        public GameObject NamePanel;

        //��ʼ����������
        public void Start()
        {
            //��ȡ��ǰ�ű������ص���Ϸ�����Canvas���
            MainCanvas = GetComponent<Canvas>();
            bool flag = false;

            //����Persistent Scene�ĳ�������ǰ������
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i) == SceneManager.GetSceneByName("Persistent Scene"))
                    flag = true;
            }
            if (!flag) SceneManager.LoadSceneAsync("Persistent Scene", LoadSceneMode.Additive);
        }

        //���"����Ϸ"ʱ��������Ϸ�Ľű�
        public void BtnNewStory()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            NamePanel.SetActive(true);
        }

        //���"�˳���Ϸ"��ťʱ�����˳�����Ľű�
        public void BtnExit()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            Application.Quit();
        }

        //���"������Ϸ"��ťʱ�������س����Ľű�
        public void BtnLoad()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            GlobalSystem.LoadGame();
        }

        //������汾��Ϣ����ťʱ������ʾInfoCanvas������MainCanvas�����
        public void BtnInfo()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            InfoCanvas.SetActive(true);
            MainCanvas.enabled = false;
            
        }

        //�������Ϸ���á���ťʱ����ʹ��SettingCanvas������MainCanvas
        public void BtnSettings()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            SettingCanvas.SetActive(true);
            MainCanvas.enabled = false;
            
        }
    }
}
