using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.SL
{
    [System.Serializable]
    public class SaveData 
    {
        public Transform PlayerTransform;        //角色位置(目前没用)
        public List<InventoryItem> PlayerBagData;  //角色背包数据
        public int ChapterIndex;                 //所处章节
        public string ItemDisplayData;           //物品隐藏数据
    }

}
