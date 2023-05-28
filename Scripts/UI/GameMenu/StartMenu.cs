using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //方便处理场景相关的操作：加载、卸载、管理、切换、监测


//自定义VGF.SceneSystem，增加配置场景的多种功能
namespace VGF.SceneSystem
{
    //继承了MonoBehaviour基类，方便调用
    public class StartMenu : MonoBehaviour
    {
        public GameObject InfoCanvas;       //存储一个游戏对象（Canvas）
        public GameObject SettingCanvas;    //存储设置面板Canvas游戏对象
        private Canvas MainCanvas;          //存储当前脚本所挂载的游戏对象的 Canvas 组件

        //初始化场景设置
        public void Start()
        {
            //获取当前脚本所挂载的游戏对象的Canvas组件
            MainCanvas = GetComponent<Canvas>();
            bool flag = false;

            //添加Persistent Scene的场景到当前场景中
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i) == SceneManager.GetSceneByName("Persistent Scene"))
                    flag = true;
            }
            if (!flag) SceneManager.LoadSceneAsync("Persistent Scene", LoadSceneMode.Additive);
        }

        //点击"新游戏"时触发新游戏的脚本
        public void BtnNewStory()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            GlobalSystem.NewGame();
        }

        //点击"退出游戏"按钮时触发退出程序的脚本
        public void BtnExit()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            Application.Quit();
        }

        //点击"载入游戏"按钮时触发加载场景的脚本
        public void BtnLoad()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            GlobalSystem.LoadGame();
        }

        //点击“版本信息”按钮时触发显示InfoCanvas并隐藏MainCanvas的组件
        public void BtnInfo()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            InfoCanvas.SetActive(true);
            MainCanvas.enabled = false;
            
        }

        //点击“游戏设置”按钮时触发使能SettingCanvas并隐藏MainCanvas
        public void BtnSettings()
        {
            SoundManager.Instance.PlaySound(Globals.Button);
            SettingCanvas.SetActive(true);
            MainCanvas.enabled = false;
            
        }
    }
}
