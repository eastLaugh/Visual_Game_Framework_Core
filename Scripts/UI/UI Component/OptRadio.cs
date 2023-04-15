using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VGF.UI
{
    public class OptRadio : OptGenericBase<int>
    {
       
        private void Start()
        {
            Init();
        }
        public override void Init()
        {
            Value = Settings.GetRBVal(name);
            SetOptUI();
        }
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
        public void RefreshOptUI()
        {
            GetOptUI();
            SetOptUI();
        }
        public void ChangeSettings()
        {
            Settings.ChangeOptRadio(name, Value);
        }

   
    }
}

