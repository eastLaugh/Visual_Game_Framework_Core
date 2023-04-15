using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

namespace VGF.Inventory
{
    public class SlotUI : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
    {
        private ItemDetails itemDetails;
        public bool Selected = false;
        public Image image;
        public Text itemName;
        public Text itemAmount;
        public SlotType slotType;
        public ItemDetailUI itemDetailUI;
        public int slotIndex;
        private void Start()
        {
            //itemDetails = new ItemDetails();
            image = GetComponentInChildren<Image>();  
        }

        
        void Update()
        {

        }
        /// <summary>
        /// ¸üÐÂBar
        /// </summary>
        public void UpdateItemBar(ItemDetails item,int amount)
        {
            itemDetails = item;
            itemName.text = itemDetails.itemName;
            itemAmount.text = amount.ToString();
        }
        private void OnDestroy()
        {
            itemDetails = null;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            EventHandler.CallChangeItemBarSelected(this);
            Selected = true;
            var color = image.color;
            color.a = 0.5f;
            image.color = color;
            Debug.Log(itemDetails.itemName);
            itemDetailUI.Display(itemDetails);
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!Selected)
            {
                var color = image.color;
                color.a = 0.8f;
                image.color = color;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Selected)
            {
                var color = image.color;
                color.a = 1f;
                image.color = color;
            }
        }
    }
}

