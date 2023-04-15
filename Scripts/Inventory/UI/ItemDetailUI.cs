using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Image icon;
    public void Display(ItemDetails item)
    {
        nameText.text = item.itemName;
        descriptionText.text = item.Description;
        icon.sprite = item.itemIcon;
        if(item.itemIcon != null)
            icon.gameObject.SetActive(true);
    }

    public void Clear()
    {
        nameText.text=null ;
        descriptionText.text = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }

}
