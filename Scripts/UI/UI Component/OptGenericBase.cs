using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�Զ���VGF.UI�⣬Ϊ������Ҫ��"Value"���Ե����ṩһ��ͨ�õĻ���
namespace VGF.UI
{
    public abstract class OptGenericBase<T> : Opt
    {
        public T Value;
    }
}
