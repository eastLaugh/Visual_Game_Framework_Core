using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�Զ���VGF.UI�⣬���Ӹ��ݻ�������ȡ��ǰֵ���洢�Ĺ���
namespace VGF.UI
{
    //�̳���Opt��
    public class OptI : OptGenericBase<int>
    {
        private Slider slider;
        
        //��ʼ������ؼ�
        public override void Init()
        {
            SetOptUI();                     //������ؼ���ֵ����Ϊ����Value���Ե�ֵ
        }
        
        /// <summary>
        /// �Ḳ�ǻ����Set
        /// </summary>
        public void Set(float Val)          //�����ݽ�����Val����ת���������������¸�ֵ���
        {
            Value = Convert.ToInt32(Val);
            SetOptUI();
        }

        //�ӿؼ��л�ȡֵ��������ؼ���ֵ����Value����
        public override void GetOptUI()
        {
            Value = Convert.ToInt32(slider.value);
        }

        //���ÿؼ���ֵ�������ǽ�Value���Ե�ֵ��������ؼ�
        public override void SetOptUI()
        {
            slider.value = Value;
        }
    }
}
