using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�Զ���VGF.UI�⣬����ʵ�ֵ�ѡ��Ĺ���
namespace VGF.UI
{
    public class OptRadio : OptGenericBase<int>
    {
        //��ʼ���ؼ�
        private void Start()
        {
            Init();
        }

        //�������ϵĵ�ѡ�����óɶ�Ӧ��״̬
        public override void Init()
        {
            Value = Settings.GetRBVal(name);
            SetOptUI();
        }

        //��鲢���µ�ǰ��ѡ���ѡ��״̬
        public override void GetOptUI()
        {
            int childCount = transform.childCount;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    GameObject gameObject = transform.GetChild(i).gameObject;
                    Toggle component = gameObject.GetComponent<Toggle>();
                    if (component.isOn && Value != i)
                    {
                        Value = i;
                        break;
                    }
                }
            }
        }

        //��Value��Ӧ�ĵ�ѡ������Ϊѡ��״̬��������ѡ������Ϊδѡ��״̬
        public override void SetOptUI()
        {
            int childCount = transform.childCount;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Toggle component = transform.GetChild(i).GetComponent<Toggle>();
                    if (component != null)
                    {
                        component.isOn = (i == Value);
                    }
                }
            }
        }

        //ˢ�µ�ѡ���״̬��ͬ����������
        public void RefreshOptUI()
        {
            GetOptUI();
            SetOptUI();
        }

        //�����ý����"Ӧ��"��ť�����ʱ��Value��ֵ���µ�Settings��
        public void ChangeSettings()
        {
            Settings.ChangeOptRadio(name, Value);
        }
    }
}
