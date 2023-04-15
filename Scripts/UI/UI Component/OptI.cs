using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VGF.UI
{
    public class OptI : OptGenericBase<int>
    {
        private Slider slider;
        public override void Init()
        {
            SetOptUI();
        }
        /// <summary>
        /// 会覆盖基类的Set
        /// </summary>
        public void Set(float Val)
        {
            Value = Convert.ToInt32(Val);
            SetOptUI();
        }
        public override void GetOptUI()
        {
            Value = Convert.ToInt32(slider.value);
        }

        public override void SetOptUI()
        {
            slider.value = Value;
        }

    }

}
