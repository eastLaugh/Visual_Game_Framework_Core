using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//自定义VGF.SL库，增加保存游戏中存档数据的功能
namespace VGF.SL
{
    [System.Serializable]
    
    //定义可序列化的类用于游戏数据的存档
    public class SaveData 
    {
        public Transform PlayerTransform;           //角色的位置(目前未投入使用)
        public List<InventoryItem> PlayerBagData;   //角色背包的数据
        public int ChapterIndex;                    //所处章节的序列
        public string ItemDisplayData;              //物品隐藏的数据
        public string PlayerName;
        public int PlayerHP;
        public int PlayerMaxHP;
    }
}
