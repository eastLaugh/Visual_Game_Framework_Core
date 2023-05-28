using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using VGF.SceneSystem;              //���ñ�������Զ���ĳ���ϵͳ


//������ͣ�������ͣ/�ָ���Ϸ��ʱ�����٣�
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    
    //�����ͣ��ťʱ��������ͣ�˵�������ͣ��Ϸ
    public void PauseButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    //���������ťʱ��ʧ����ͣ�˵����ָ���Ϸ��������
    public void ResumeButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }

    //����������˵���ťʱ��ʧ����ͣ�˵����ָ���Ϸ�������в����س���
    public void MainMenuButtonClicked()
    {
        SoundManager.Instance.PlaySound(Globals.Button);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        Autumn.Harvest<SceneLoader>().SwitchSceneByName("GameMenu");
    }
}
