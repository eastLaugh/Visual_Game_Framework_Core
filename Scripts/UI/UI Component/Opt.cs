using System;
using UnityEngine;


//�Զ���VGF.UI������ʵ��ѡ��UI������
namespace VGF.UI
{
    public abstract class Opt : MonoBehaviour
    {
        public abstract void GetOptUI();    //��ȡ
        public abstract void SetOptUI();    //����
        public abstract void Init();        //��ʼ��
    }
}
