using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;

// [RequireComponent(typeof(CarryUI))]
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
        Msg.Show(text,gameObject);
    }
}
