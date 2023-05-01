using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
[RequireComponent(typeof(Character))]


//该类实现了显示一个带有文本的消息框，显示在与物体碰撞的玩家角色上方的功能
public class Say : MonoBehaviour, ICharacter
{
    public Canvas Canvas;

    //创建一个Canvas，挂载在该脚本所在的物体下
    private void OnEnable()
    {
        GameObject canvasGO = new GameObject("Canvas");
        canvasGO.transform.SetParent(transform);

        Canvas = canvasGO.AddComponent<Canvas>();
        Canvas.renderMode = RenderMode.WorldSpace;
        Canvas.worldCamera = Camera.main;

        RectTransform rectTransform = canvasGO.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one;
    }

    public void OnInteract()
    {
        //
    }

    public void OnPlayerEnter()
    {
        //
    }

    public void OnPlayerExit()
    {
        //
    }

    //显示文本
    internal void SayMsg(string text)
    {
        //一般来说，Msg应该显示在碰撞体的上方1/5间隔处
        Collider col = GetComponent<Collider>();            //获取物体的碰撞体
        
        if (col == null)
        {
            Debug.LogError("没有找到碰撞体，故不知道在哪里显示Msg");
        }
        
        Bounds bounds = col.bounds;

        //通过Msg类显示文本
        Msg.Show(text, gameObject, new Vector3(transform.position.x, bounds.max.y, transform.position.z));
    }
}
