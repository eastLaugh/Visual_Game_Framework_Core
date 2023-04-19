using System;
using UnityEngine;


//自定义VGF.UI，增加实现选项UI的设置
namespace VGF.UI
{
    public abstract class Opt : MonoBehaviour
    {
        public abstract void GetOptUI();    //获取
        public abstract void SetOptUI();    //设置
        public abstract void Init();        //初始化
    }
}
