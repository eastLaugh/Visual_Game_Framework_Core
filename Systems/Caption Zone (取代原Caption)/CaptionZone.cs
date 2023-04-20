using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Pool;
using System;
using DG.Tweening;      //���ڶ����������͹������ڶ����Ŀ���
using TMPro;            //�ı���ʾ�⣬���ṩ���๦�ܣ�����ߵ��ı������ȡ�֧�ָ��ı��ȵ�


[Beans]
//�����ı��Ķ���أ�����Bean��һ�����
public class CaptionZone : MonoBehaviour
{
    private IObjectPool<TextMeshProUGUI> pool;
    public Canvas canvas;
    public GameObject FloatingText;

    //��ʼ��ע�벢�洢�����
    private void Awake()
    {
        this.Bean();
        pool = new ObjectPool<TextMeshProUGUI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
    }

    private void Start()
    {
        //���Ի�����
    }

    //�����¶�����Ϊ����ص�Ԫ��
    private TextMeshProUGUI CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(FloatingText, canvas.transform);
        return gameObject1.GetComponent<TextMeshProUGUI>();
    }

    //��ȡ�������Ԫ��ʱ�����ã����ö���Ϊ�ɼ�
    private void OnTakeFromPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(true);
    }

    //�ڶ��󱻹黹����ʱ���ã����ö���Ϊ���ɼ�
    private void OnReturnedToPool(TextMeshProUGUI obj)
    {
        obj.gameObject.SetActive(false);
    }

    //�ڶ������ʱ���ã������־��Ϣ
    private void OnDestroyPoolObject(TextMeshProUGUI obj)
    {
        Debug.Log("OnDestroyPoolObject");
    }

    //������ʾ�͵��ø������ֳ�
    void OnGUI()
    {
        GUILayout.Label("Pool size: " + pool.CountInactive, new GUIStyle(GUI.skin.label) { fontSize = 60 });
        //�����ť���������һ��������λ�õĸ�������
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

    /*��ȡ�����������Ժ�λ�ã�ͨ��Tweenerʵ���ı��ĸ���Ч��
    ����������黹����أ������ı�͸���ȱ仯Ч��*/
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
