using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;


//���������ڽ�������ʾ��Ϣ
[ManualBean]
public class Msg : UICollection
{
    public GameObject CanvasPrefab;

    //ע������ΪBean����
    protected override void Awake()
    {
        base.Awake();
        this.Bean();
    }
    private void OnDestroy()
    {
        this.UnBean();
    }
    //���½������������
    protected override void OnCreate(GameObject newlyCreatedObject)
    {
        newlyCreatedObject.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            pool.Release(newlyCreatedObject);
        });
    }
    
    [Autowired]
    private static Msg msg;
    
    //��ָ��λ�ô���MsgԤ���壬��������ʾ���ı����������λ��
    public static void Show(string text, GameObject parent, Vector3 postion)
    {
        GameObject gameObject1 = msg.pool.Get();
        gameObject1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        gameObject1.transform.SetParent(parent.transform);
        gameObject1.transform.localPosition = new Vector3(0, 0, 0);
        gameObject1.transform.position = postion;
        //gameObject1.GetComponent<Canvas>().sortingLayerName = "Word";
        //gameObject1.GetComponent<Canvas>().sortingOrder= 100;
        

    }

    protected override GameObject CreatePooledItem()
    {
        GameObject gameObject1 = Instantiate(Prefab,transform);
        OnCreate(gameObject1);
        return gameObject1;
    }
}
