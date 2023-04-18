using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*�Զ���̳���MonoBehaviour�����InfoCanvas��
���ڿ���UI�����еİ汾��Ϣ������ʾ������*/
public class InfoCanvas : MonoBehaviour
{
    public Canvas mainCanvas;           //������UI��������ʾ������
    public GameObject VersionPanel;     //��ʾ�汾��Ϣ���
    
    void Start()
    {
        //�ɸ��Ի��༭
    }
    
    //��Ӧ�汾��Ϣ���ĵ����ť�¼�����ʾ�汾��Ϣ���
    public void BtnVersion()
    {
        VersionPanel.SetActive(true);
    }

    //��Ӧ�汾��Ϣ���Ĺرհ�ť�¼������ذ汾��Ϣ���
    public void BtnClose()
    {
        VersionPanel.SetActive(false);
    }

    //��Ӧ�汾��Ϣ����еķ��ذ�ť�ĵ���¼�����ʾ��UI���������ص�ǰ����
    public void BtnBack()
    {
        mainCanvas.enabled = true;
        gameObject.SetActive(false);
    }
}
