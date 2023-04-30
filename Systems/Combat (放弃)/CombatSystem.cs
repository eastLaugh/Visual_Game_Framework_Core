using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AutumnFramework;

[Bean]
//战斗系统，供自定义个性化开发
public class CombatSystem : MonoBehaviour
{

    // int i=0;
    public CombatSystem()
    {
        //
    }
    public void Run(Enemy enemy, Action<string> callback)
    {
        Debug.Log("Here you are!!");    //测试程序
    }

    private void Start()
    {
        //
    }

    private void Awake()
    {
        //
    }

    void Autumn()
    {
        //
    }

    //在异步加载场景时切换到战斗场景
    IEnumerator SwitchToCompatScene()
    {
        yield return SceneManager.LoadSceneAsync("Combat", LoadSceneMode.Additive);
    }
}
