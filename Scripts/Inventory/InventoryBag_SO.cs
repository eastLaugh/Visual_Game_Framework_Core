using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
�Զ������ԣ������˵����������ͱ༭PlayerBag_SO
����������
1.��Unity�༭�����Ҽ�������Դ�����е��ļ���
2.�ڵ����Ĵ�����ѡ��Visual Game Framework/PlayerBag_SO
*/
[CreateAssetMenu(fileName = "PlayerBag_SO", menuName = "Visual Game Framework/PlayerBag_SO")]


//����������洢������Ʒ���嵥
public class InventoryBag_SO : ScriptableObject
{
    public List<InventoryItem> itemList;
}
