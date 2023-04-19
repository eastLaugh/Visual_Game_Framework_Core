using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//自定义VGF.UI库，增加实现单选框的功能
namespace VGF.UI
{
    public class OptRadio : OptGenericBase<int>
    {
        //初始化控件
        private void Start()
        {
            Init();
        }

        //将界面上的单选框设置成对应的状态
        public override void Init()
        {
            Value = Settings.GetRBVal(name);
            SetOptUI();
        }

        //检查并更新当前单选框的选中状态
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

        //将Value对应的单选框设置为选中状态，其他单选框设置为未选中状态
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

        //刷新单选框的状态并同步到界面上
        public void RefreshOptUI()
        {
            GetOptUI();
            SetOptUI();
        }

        //在设置界面的"应用"按钮被点击时将Value的值更新到Settings中
        public void ChangeSettings()
        {
            Settings.ChangeOptRadio(name, Value);
        }
    }
}
