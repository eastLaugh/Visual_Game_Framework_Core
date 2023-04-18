using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //引入Unity官方的基础框架，可以方便处理UI的基础事件（比如点击、拖拽……）
using UnityEngine.UI;
//using UnityEngine.UIElements;


//自定义VGF.Inventory，提供鼠标点击、悬停和离开时触发事件的接口
namespace VGF.Inventory
{
    //继承多个基类方便调用实现多种功能
    public class SlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //定义背包物品的多个判定和显示指标
        private ItemDetails itemDetails;
        public bool Selected = false;
        public Image image;
        public Text itemName;
        public Text itemAmount;
        public SlotType slotType;
        public ItemDetailUI itemDetailUI;
        public int slotIndex;
        
        //将image初始化为该Slot子物体的Image组件
        private void Start()
        {
            //itemDetails = new ItemDetails();
            image = GetComponentInChildren<Image>();
        }

        //可加入个性化自定义的刷新内容
        void Update()
        {
            //
        }
        
        /// <summary>
        /// 更新Bar
        /// </summary>
        public void UpdateItemBar(ItemDetails item, int amount)     //更新并获取物品的名称和数量
        {
            itemDetails = item;
            itemName.text = itemDetails.itemName;
            itemAmount.text = amount.ToString();
        }
        
        //将itemDetils置空，便于下一个物品的显示
        private void OnDestroy()
        {
            itemDetails = null;
        }

        //在背包槽位被点击时触发，将物品的itemDetails对象传递给Display()函数
        public void OnPointerDown(PointerEventData eventData)
        {
            EventHandler.CallChangeItemBarSelected(this);
            Selected = true;
            var color = image.color;
            color.a = 0.5f;                     //设置image的透明度设置为50%
            image.color = color;
            Debug.Log(itemDetails.itemName);
            itemDetailUI.Display(itemDetails);

        }

        //当鼠标悬停在背包槽位时触发，设置图像显示的透明度，突出显示
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Selected)
            {
                var color = image.color;
                color.a = 0.8f;                 //设置image的透明度为80%
                image.color = color;
            }
        }

        //当鼠标从槽位上移开的时候触发，设置图像的透明度，恢复正常显示的样子
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Selected)
            {
                var color = image.color;
                color.a = 1f;                   //设置image的透明度为100%，即不透明
                image.color = color;
            }
        }
    }
}
