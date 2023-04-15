using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InfoCanvas : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject VersionPanel;
    void Start()
    {
        
    }
    public void BtnVersion()
    {
        VersionPanel.SetActive(true);
    }
    public void BtnClose()
    {
        VersionPanel.SetActive(false);
    }
    public void BtnBack()
    {
        mainCanvas.enabled = true;
        gameObject.SetActive(false);
    }
}
