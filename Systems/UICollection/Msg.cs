using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;


//该类用于在界面上显示消息
[ManualBean]
public class Msg : UICollection
{
    public GameObject CanvasPrefab;

    //注册自身为Bean对象
    protected override void Awake()
    {
        base.Awake();
        this.Bean();
    }

    //对新建对象进行设置
    protected override void OnCreate(GameObject newlyCreatedObject)
    {
        newlyCreatedObject.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            pool.Release(newlyCreatedObject);
        });
    }
    
    [Autowired]
    private static Msg msg;
    
    //在指定位置创建Msg预制体，并设置显示的文本、父对象和位置
    public static void Show(string text, GameObject parent, Vector3 postion)
    {
        GameObject gameObject1 = msg.pool.Get();
        gameObject1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        gameObject1.transform.SetParent(parent.transform);
        gameObject1.transform.localPosition = new Vector3(0, 0, 0);
        gameObject1.transform.position = postion;
    }
}
s