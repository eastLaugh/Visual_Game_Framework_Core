using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//�����༭��Ϸ�˵���������
public class GameMenu : MonoBehaviour
{
    //�ڿ�ʼʱ����
    void Start()
    {
        // �ɸ��Ի�����
        // if(Application.isEditor)
        //     OnClickPlay();
    }

    //��֡����
    void Update()
    {
        //�ɸ��Ի�����
    }

    //����ǰ�����л���"Persistent Scene"������ʹ�õ���������ģʽ
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
    }

    //�洢/��������
    public static bool isNew;
    //�ɽ�һ������
}
