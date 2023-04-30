using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
[RequireComponent(typeof(Character))]

public class Say : MonoBehaviour, ICharacter
{
    public Canvas Canvas;
    private void OnEnable() {

        GameObject canvasGO = new GameObject("Canvas");
        canvasGO.transform.SetParent(transform);

        Canvas = canvasGO.AddComponent<Canvas>();
        Canvas.renderMode=RenderMode.WorldSpace;
        Canvas.worldCamera = Camera.main;

        RectTransform rectTransform = canvasGO.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one;
    }
    
    public void OnInteract()
    {

    }

    public void OnPlayerEnter()
    {
    }

    public void OnPlayerExit()
    {
    }
    internal void SayMsg(string text)
    {
        //一般来说，Msg应该显示在碰撞体的上方1/5间隔处
        Collider col = GetComponent<Collider>();
        if(col==null){
            Debug.LogError("没有找到碰撞体，故不知道在哪里显示Msg");
        }
        Bounds bounds = col.bounds;
        Msg.Show(text,gameObject,new Vector3(transform.position.x,bounds.max.y,transform.position.z));
    }
}
