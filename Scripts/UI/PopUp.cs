using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    //开始时被调用
    void Start()
    {
        //可个性化开发
        // Debug.Log(button.GetComponent<UnityEngine.UI.Image>());   // Can be found
        // Debug.Log(button.GetComponent<UnityEngine.UIElements.Image>());
    }

    //逐帧调用
    void Update()
    {
        //可个性化开发
    }

    private GameObject window => transform.Find("Canvas/Window").gameObject;
    private Animator windowsAnimator => window.GetComponent<Animator>();
    private GameObject button => transform.Find("Canvas/Window/Image").gameObject;
    
    private System.Action<int> callback;

    //弹出窗口并显示指定的消息
    /*为弹出窗口的按钮添加点击事件，同时设置弹出窗口的文本信息，并启动弹出窗口的动画*/
    public void Pop(string message, System.Action<int> callback = null)
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Close);
        GetComponentInChildren<UnityEngine.UI.Text>().text = message;
        this.callback = callback;
        windowsAnimator.SetTrigger("pop");    //设置回调函数

    }

    //初始化
    void Init()
    {
        //可个性化开发
    }

    //弹出窗口用户选择的项目，包括关闭和接收
    public enum PopUpType { close, receive }

    //关闭弹出窗口
    public void Close()
    {
        windowsAnimator.SetTrigger("close");
        StartCoroutine("wait");
    }

    //响应用户操作，处理用户选择
    public void Receive()
    {
        windowsAnimator.SetTrigger("close");
        StartCoroutine("wait");
    }

    //回调函数
    IEnumerator wait(int type = -1)
    {
        while (!windowsAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return null;
        }
        //播放完毕
        callback?.Invoke(type);
    }
}
