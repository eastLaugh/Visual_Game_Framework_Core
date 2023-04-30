using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Pool;
using System;
using DG.Tweening;      //用于动画的制作和管理，便于动画的开发
using TMPro;            //文本显示库，能提供更多功能，如更高的文本清晰度、支持富文本等等


//该类包含了对象池的相关逻辑和一些常用的UI控件
public abstract class UICollection : MonoBehaviour
{
    protected IObjectPool<GameObject> pool;     //存储Prefab实例化的对象
    [SerializeField]
    protected Canvas Canvas;                    //确定对象池中实例化的对象的父物体
    [SerializeField]
    protected GameObject Prefab;                //作为对象池中实例化的对象的预设体

    public GameObject Get => pool.Get();        //从对象池中获取一个对象
    
    protected virtual void Awake()
    {
        Debug.Log(233);

        pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }

    //销毁对象池中的对象时执行的内容
    protected virtual void OnDestroyPoolObject(GameObject obj)
    {
        //
    }

    //将对象设为不可见
    protected virtual void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    //将对象设为可见
    protected virtual void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);

    }

    //创建对象池中的对象（实例化对象、设定父物体和执行用户自定义逻辑）
    private GameObject CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(Prefab, Canvas.transform);
        OnCreate(gameObject1);
        return gameObject1;
    }

    protected abstract void OnCreate(GameObject newlyCreatedObject);
}
