using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;

[ManualBean]
public class Msg : UICollection
{
    public GameObject CanvasPrefab;
    protected override void Awake() {
        base.Awake();
        this.Bean();
    }

    protected override void OnCreate(GameObject newlyCreatedObject)
    {
        newlyCreatedObject.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            pool.Release(newlyCreatedObject);
        });
    }
    [Autowired]
    private static Msg msg;
    public static void Show(string text,GameObject parent,Vector3 postion){
        GameObject gameObject1 = msg.pool.Get();
        gameObject1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text=text;
        gameObject1.transform.SetParent(parent.transform);
        gameObject1.transform.localPosition=new Vector3(0,0,0);
        gameObject1.transform.position = postion;
    }
}
