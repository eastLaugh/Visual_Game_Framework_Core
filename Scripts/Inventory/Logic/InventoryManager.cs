using System.Collections;           //该命名空间包含C#中许多常用的数据结构和算法
using System.Collections.Generic;   //同上
using UnityEngine;


//自定义命名空间，该类用于管理玩家背包中的物品，供其他模块系统引用
namespace VGF.Inventory
{
    //继承Singleton<InventoryManager>，便于其他类引用这两个类的内容
    public class InventoryManager : Singleton<InventoryManager>
    {
        /*在背包中，每种物品都是一个InventoryItem对象；
        每个物品包含两个字段：itemID表示物品的ID，amount表示物品的数量；
        背包中所有物品的信息都存储在playerBag_SO对象中*/

        //定义玩家背包内的全物品属性
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO playerBag_SO;

        public void NewGame()
        {
            AddItem(1002, 1);
            //EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
        }
        /// <summary>
        /// 找到物品信息
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>物品信息</returns>
        public ItemDetails GetItemDetails(int ID)       //根据指定的背包物品的ID，获取背包内物品的详细信息
        {
            return itemDataList_SO.itemDataList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// 把物品放入背包（入口函数）
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <param name="num">物品数量</param>
        /// <returns>是否能放入背包</returns>
        public bool AddItem(int ID, int num)             //将指定数量的物品添加到背包中（作为入口函数），用bool量检测添加过程（ID和num）
        {
            int index = GetItemIndexInBag(ID);
            if (!CheckBagCapacity() && index == -1)
            {
                return false;
            }
            AddItemAtIndex(ID, index, num);
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
            return true;
        }
        public bool ReduceItem(int ID,int num)          //将指定数量的物品移除背包（作为出口函数），用bool量检测添加过程（ID和num）
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == ID && playerBag_SO.itemList[i].amount >= num)
                {
                    playerBag_SO.itemList[i].amount -= num;
                    EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
                    return true;
                }
            }
            return false;

        }
        //在背包中查找是否存在指定数量的物品，存在则返回true，不存在返回false
        public bool SearchItem(int ID, int num)
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == ID && playerBag_SO.itemList[i].amount >= num)
                {
                    return true;
                }
            }
            return false;
        }

        //通过设置物品属性，清空背包中的所有物品
        public void EmptyItems()
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                playerBag_SO.itemList[i].amount = 0;
                playerBag_SO.itemList[i].itemID = 0;
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
        }

        /// <summary>
        /// 得到物品在背包的索引值
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>物品在背包的索引值</returns>
        private int GetItemIndexInBag(int ID)           //通过传入指定物品的ID来获取该物品在背包中的索引值ID
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == ID)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>是否有空位</returns>
        private bool CheckBagCapacity()                 //遍历背包容量来检查背包是否有空位
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /*将指定数量的物品添加到背包的指定位置，需依次传入ID、位置、数量；
        该方法会自动检测背包是否有空位，若背包中没有空位则不会添加物品*/
        private void AddItemAtIndex(int ID, int index, int amount)
        {
            if (index == -1)
            {
                var item = new InventoryItem { itemID = ID, amount = amount };
                for (int i = 0; i < playerBag_SO.itemList.Count; i++)
                {
                    if (playerBag_SO.itemList[i].itemID == 0)
                    {
                        playerBag_SO.itemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                int currentAmount = playerBag_SO.itemList[index].amount + amount;
                var item = new InventoryItem { itemID = ID, amount = currentAmount };
                playerBag_SO.itemList[index] = item;
            }
        }

        //程序开始时会调用，用于更新背包的UI界面
        void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
            EventHandler.NewGame += NewGame;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventHandler.NewGame -= NewGame;
        }
        //每一帧都会调用，实现实时更新
        void Update()
        {
            //可继续开发加入多样的更新时候的功能
        }
    }
}
