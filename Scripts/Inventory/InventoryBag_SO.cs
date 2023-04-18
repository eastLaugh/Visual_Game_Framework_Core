using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
自定义属性，创建菜单项来创建和编辑PlayerBag_SO
创建操作：
1.在Unity编辑器中右键单击资源窗口中的文件夹
2.在弹出的窗口中选择Visual Game Framework/PlayerBag_SO
*/
[CreateAssetMenu(fileName = "PlayerBag_SO", menuName = "Visual Game Framework/PlayerBag_SO")]


//定义该类来存储所有物品的清单
public class InventoryBag_SO : ScriptableObject
{
    public List<InventoryItem> itemList;
}
