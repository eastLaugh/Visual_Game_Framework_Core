using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AutumnFramework;

[Bean]
//ս��ϵͳ�����Զ�����Ի�����
public class CombatSystem : MonoBehaviour
{

    // int i=0;
    public CombatSystem()
    {
        //
    }
    public void Run(Enemy enemy, Action<string> callback)
    {
        Debug.Log("Here you are!!");    //���Գ���
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

    //���첽���س���ʱ�л���ս������
    IEnumerator SwitchToCompatScene()
    {
        yield return SceneManager.LoadSceneAsync("Combat", LoadSceneMode.Additive);
    }
}
