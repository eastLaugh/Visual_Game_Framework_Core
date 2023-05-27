using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//序列化物品属性
[System.Serializable]
//自定义物品的详细信息
public class ItemDetails
{
    public int itemID;
    public string itemName;
    public string Description;
    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite itemWorldSprite;
    public int physicAttack;
    public int magicAttack;
    public int physicDefence;
    public int magicDefence;
    public int hPRecover;
    public int mpRecover;
    public int sellPrice;
    public int BuyPrice;
}


[System.Serializable]
//记录某物品在背包中的数量
public class InventoryItem
{
    public int itemID;
    public int amount;
}

[System.Serializable]
//记录技能指标
public class Skill
{
    public string Name;
    public string Description;
    public float DurationTime;
    public bool Unlocked;
    public bool Avaliable;
    public Sprite sprite;
}
