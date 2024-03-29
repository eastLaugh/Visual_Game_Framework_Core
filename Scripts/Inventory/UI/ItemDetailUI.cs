using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//自定义继承了MonoBehaviour的ItemDetailUI类，用于实现物品详细信息的UI界面
public class ItemDetailUI : MonoBehaviour
{
    //定义物品的属性：名称、描述、图标
    public Text nameText;
    public Text descriptionText;
    public Image icon;

    //传入物品的item，判断并显示该物品的名称、描述、图标
    public void Display(ItemDetails item)
    {
        nameText.text = item.itemName;
        descriptionText.text = item.Description;
        icon.sprite = item.itemIcon;
        
        //物品图标不存在时，隐藏该图标
        if (item.itemIcon != null)
            icon.gameObject.SetActive(true);
    }

    //判断并清除显示的物品信息，如果物品图标不存在，则继续隐藏图标
    public void Clear()
    {
        nameText.text = null;
        descriptionText.text = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
