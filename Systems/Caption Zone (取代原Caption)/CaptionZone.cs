using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Pool;
using System;
using DG.Tweening;      //用于动画的制作和管理，便于动画的开发
using TMPro;            //文本显示库，能提供更多功能，如更高的文本清晰度、支持富文本等等


[Beans]
//管理文本的对象池，它是Bean的一个组件
public class CaptionZone : MonoBehaviour
{
    private IObjectPool<TextMeshProUGUI> pool;
    public Canvas canvas;
    public GameObject FloatingText;

    //初始化注入并存储对象池
    private void Awake()
    {
        this.Bean();
        pool = new ObjectPool<TextMeshProUGUI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }

    private void Start()
    {
        //个性化开发
    }

    //创建新对象作为对象池的元素
    private TextMeshProUGUI CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(FloatingText, canvas.transform);
        return gameObject1.GetComponent<TextMeshProUGUI>();
    }

    //在取出对象池元素时被调用，设置对象为可见
    private void OnTakeFromPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(true);
    }

    //在对象被归还池中时调用，设置对象为不可见
    private void OnReturnedToPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(false);
    }

    //在对象被清除时调用，输出日志信息
    private void OnDestroyPoolObject(TextMeshProUGUI obj)
    {
        Debug.Log("OnDestroyPoolObject");
    }

    //用于显示和调用浮动文字池
    void OnGUI()
    {
        GUILayout.Label("Pool size: " + pool.CountInactive, new GUIStyle(GUI.skin.label) { fontSize = 60 });
        //点击按钮会随机生成一定数量和位置的浮动文字
        if (GUILayout.Button("Create Particles", new GUIStyle(GUI.skin.button) { fontSize = 60 }))
        {
            var amount = UnityEngine.Random.Range(1, 3);
            for (int i = 0; i < amount; ++i)
            {
                Float(new Vector2(UnityEngine.Random.Range(Screen.width / -2f, Screen.width / 2f), UnityEngine.Random.Range(Screen.height / -2f, Screen.height / 2f))
                , UnityEngine.Random.Range(99, 999).ToString());
            }
        }
    }

    /*获取对象设置属性和位置，通过Tweener实现文本的浮动效果
    动画结束后归还对象池，设置文本透明度变化效果*/
    void Float(Vector3 anchoredPosition, string text)
    {
        TextMeshProUGUI tmp = pool.Get();
        tmp.SetText(text);
        tmp.rectTransform.anchoredPosition = anchoredPosition;
        float second = 2f;
        Tweener tweener = tmp.rectTransform.DOLocalMoveY(tmp.rectTransform.anchoredPosition.y + Screen.height / 8f, second).SetEase(Ease.InOutQuad);
        tmp.alpha = 1;
        tmp.DOFade(0, second).SetEase(Ease.InOutQuad);
        tweener.OnComplete(() =>
        {
            pool.Release(tmp);
        });
    }
}
