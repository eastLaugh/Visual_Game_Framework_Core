using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�Զ���VGF.UI����������ʾ��Ϣ�Ľ���
namespace VGF.UI
{
    //�̳л��࣬���ڵ���
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private Text hintText;
        //�����ʾ��Ϣ��������ʾ��Ϣ��ʾ�Ľ���
        public void HintEnd()
        {
            hintText.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}
