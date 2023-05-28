using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using VGF.SceneSystem;              //引用本框架下自定义的场景系统


//设置暂停界面和暂停/恢复游戏（时间流速）
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    
    //点击暂停按钮时，激活暂停菜单，并暂停游戏
    public void PauseButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    //点击继续按钮时，失能暂停菜单，恢复游戏正常运行
    public void ResumeButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }

    //点击返回主菜单按钮时，失能暂停菜单，恢复游戏正常运行并加载场景
    public void MainMenuButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        Autumn.Harvest<SceneLoader>().SwitchSceneByName("GameMenu");
    }
}
