using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;
using System;
using DG.Tweening;

[Beans]
public class CaptionZone : MonoBehaviour
{
    private IObjectPool<TextMeshProUGUI> pool;
    public Canvas canvas;
    public GameObject FloatingText;

    private void Awake()
    {
        this.Bean();
        pool = new ObjectPool<TextMeshProUGUI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }
    private void Start() {
        
    }
    private TextMeshProUGUI CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(FloatingText, canvas.transform);
        return gameObject1.GetComponent<TextMeshProUGUI>();
    }

    private void OnTakeFromPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(TextMeshProUGUI obj)
    {
        Debug.Log("OnDestroyPoolObject");
    }


    void OnGUI()
    {
        GUILayout.Label("Pool size: " + pool.CountInactive, new GUIStyle(GUI.skin.label) { fontSize = 60 });
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

