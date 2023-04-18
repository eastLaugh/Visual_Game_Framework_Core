using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//�Զ���̳���MonoBehaviour��ItemDetailUI�࣬����ʵ����Ʒ��ϸ��Ϣ��UI����
public class ItemDetailUI : MonoBehaviour
{
    //������Ʒ�����ԣ����ơ�������ͼ��
    public Text nameText;
    public Text descriptionText;
    public Image icon;

    //������Ʒ��item���жϲ���ʾ����Ʒ�����ơ�������ͼ��
    public void Display(ItemDetails item)
    {
        nameText.text = item.itemName;
        descriptionText.text = item.Description;
        icon.sprite = item.itemIcon;
        
        //��Ʒͼ�겻����ʱ�����ظ�ͼ��
        if (item.itemIcon != null)
            icon.gameObject.SetActive(true);
    }

    //�жϲ������ʾ����Ʒ��Ϣ�������Ʒͼ�겻���ڣ����������ͼ��
    public void Clear()
    {
        nameText.text = null;
        descriptionText.text = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
