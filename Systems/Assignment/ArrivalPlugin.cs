using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Assignment;


//ʵ�ֽ�ɫ����ĳ�غ����Ϸ��Ϊ
public class ArrivalPlugin : MonoBehaviour
{
    public Arrival arrival;

    //���¼���������ʹ������󱻱��Ϊ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arrival.Ticked();
        }
    }
}
