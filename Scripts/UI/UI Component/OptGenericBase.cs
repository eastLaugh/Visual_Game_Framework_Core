using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//自定义VGF.UI库，为所有需要有"Value"属性的类提供一个通用的基类
namespace VGF.UI
{
    public abstract class OptGenericBase<T> : Opt
    {
        public T Value;
    }
}
