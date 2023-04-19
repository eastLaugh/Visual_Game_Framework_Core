using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//初步编辑游戏菜单按键功能
public class GameMenu : MonoBehaviour
{
    //在开始时调用
    void Start()
    {
        // 可个性化开发
        // if(Application.isEditor)
        //     OnClickPlay();
    }

    //逐帧调用
    void Update()
    {
        //可个性化开发
    }

    //将当前场景切换到"Persistent Scene"，并且使用单场景加载模式
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
    }

    //存储/加载数据
    public static bool isNew;
    //可进一步开发
}
