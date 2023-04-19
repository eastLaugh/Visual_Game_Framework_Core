using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//自定义VGF.UI库，增加根据滑块来获取当前值并存储的功能
namespace VGF.UI
{
    //继承了Opt类
    public class OptI : OptGenericBase<int>
    {
        private Slider slider;
        
        //初始化滑块控件
        public override void Init()
        {
            SetOptUI();                     //将滑块控件的值设置为类中Value属性的值
        }
        
        /// <summary>
        /// 会覆盖基类的Set
        /// </summary>
        public void Set(float Val)          //将传递进来的Val参数转换成整数类型重新赋值输出
        {
            Value = Convert.ToInt32(Val);
            SetOptUI();
        }

        //从控件中获取值，将滑块控件的值赋给Value属性
        public override void GetOptUI()
        {
            Value = Convert.ToInt32(slider.value);
        }

        //设置控件的值，这里是将Value属性的值赋给滑块控件
        public override void SetOptUI()
        {
            slider.value = Value;
        }
    }
}
