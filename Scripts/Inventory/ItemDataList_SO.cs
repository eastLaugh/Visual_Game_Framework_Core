using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
自定义属性，创建菜单项来创建和编辑ItemDataList_SO
创建操作：
1.在Unity编辑器中右键单击资源窗口中的文件夹
2.在弹出的窗口中选择Visual Game Framework/ItemDataList
*/
[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Visual Game Framework/ItemDataList")]


//定义该类来存储所有物品的详细信息
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetails> itemDataList;
}
