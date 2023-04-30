using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Pool;
using System;
using DG.Tweening;      //用于动画的制作和管理，便于动画的开发
using TMPro;            //文本显示库，能提供更多功能，如更高的文本清晰度、支持富文本等等

public abstract class UICollection : MonoBehaviour
{

    protected IObjectPool<GameObject> pool;
    [SerializeField]
    protected Canvas Canvas;
    [SerializeField]
    protected GameObject Prefab;

    public GameObject Get => pool.Get();
    protected virtual void Awake()
    {
        Debug.Log(233);

        pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }

    protected virtual void OnDestroyPoolObject(GameObject obj)
    {
    }

    protected virtual void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected virtual void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);

    }

    private GameObject CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(Prefab,Canvas.transform);
        OnCreate(gameObject1);
        return gameObject1;
    }

    protected abstract void OnCreate(GameObject newlyCreatedObject);
}
