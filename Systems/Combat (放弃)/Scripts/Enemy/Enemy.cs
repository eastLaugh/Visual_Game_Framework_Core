using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���ӵ��˵Ĳ�������
public abstract class Enemy : MonoBehaviour
{
    public Sprite Bullet;   //����ʹ�õ��ӵ�

    //��ȡ���˹������ִ�
    protected Enemy()
    {
        rounds = GetRounds();
    }

    private Round[] rounds;
    public abstract Round[] GetRounds();
    // private int phase=0;

    //���˵Ĺ����߼�
    public void Run()
    {
        //�����Ի�����
    }
}
