using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace VGF.SceneSystem
{
    public class StartMenu : MonoBehaviour
    {
        public GameObject InfoCanvas;
        public GameObject SettingCanvas;
        private Canvas MainCanvas;
        public void Start()
        {
            MainCanvas = GetComponent<Canvas>();
            bool flag = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i) == SceneManager.GetSceneByName("Persistent Scene")) 
                    flag = true; 
            }
            if(!flag)  SceneManager.LoadSceneAsync("Persistent Scene", LoadSceneMode.Additive);
        }
        public void BtnNewStory()
        {
            GlobalSystem.NewGame();
        }
        public void BtnExit()
        {
            Application.Quit();
        }
        public void BtnLoad()
        {
            GlobalSystem.LoadGame();
        }
        public void BtnInfo()
        {
            InfoCanvas.SetActive(true);
            MainCanvas.enabled = false;
        }
        public void BtnSettings()
        {
            SettingCanvas.SetActive(true);
            MainCanvas.enabled = false;
        }

    }
}
