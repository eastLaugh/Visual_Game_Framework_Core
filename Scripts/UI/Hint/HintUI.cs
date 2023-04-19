using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//自定义VGF.UI，来管理提示信息的界面
namespace VGF.UI
{
    //继承基类，便于调用
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private Text hintText;
        //清空提示信息并隐藏提示信息显示的界面
        public void HintEnd()
        {
            hintText.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}
