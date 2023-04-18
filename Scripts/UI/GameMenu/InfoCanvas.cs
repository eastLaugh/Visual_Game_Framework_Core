using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*自定义继承了MonoBehaviour基类的InfoCanvas，
用于控制UI画布中的版本信息面板的显示和隐藏*/
public class InfoCanvas : MonoBehaviour
{
    public Canvas mainCanvas;           //控制主UI画布的显示与隐藏
    public GameObject VersionPanel;     //表示版本信息面板
    
    void Start()
    {
        //可个性化编辑
    }
    
    //响应版本信息面板的点击按钮事件，显示版本信息面板
    public void BtnVersion()
    {
        VersionPanel.SetActive(true);
    }

    //响应版本信息面板的关闭按钮事件，隐藏版本信息面板
    public void BtnClose()
    {
        VersionPanel.SetActive(false);
    }

    //响应版本信息面板中的返回按钮的点击事件，显示主UI画布，隐藏当前画布
    public void BtnBack()
    {
        mainCanvas.enabled = true;
        gameObject.SetActive(false);
    }
}
