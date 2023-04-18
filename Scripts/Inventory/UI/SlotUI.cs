using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //����Unity�ٷ��Ļ�����ܣ����Է��㴦��UI�Ļ����¼�������������ק������
using UnityEngine.UI;
//using UnityEngine.UIElements;


//�Զ���VGF.Inventory���ṩ���������ͣ���뿪ʱ�����¼��Ľӿ�
namespace VGF.Inventory
{
    //�̳ж�����෽�����ʵ�ֶ��ֹ���
    public class SlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //���屳����Ʒ�Ķ���ж�����ʾָ��
        private ItemDetails itemDetails;
        public bool Selected = false;
        public Image image;
        public Text itemName;
        public Text itemAmount;
        public SlotType slotType;
        public ItemDetailUI itemDetailUI;
        public int slotIndex;
        
        //��image��ʼ��Ϊ��Slot�������Image���
        private void Start()
        {
            //itemDetails = new ItemDetails();
            image = GetComponentInChildren<Image>();
        }

        //�ɼ�����Ի��Զ����ˢ������
        void Update()
        {
            //
        }
        
        /// <summary>
        /// ����Bar
        /// </summary>
        public void UpdateItemBar(ItemDetails item, int amount)     //���²���ȡ��Ʒ�����ƺ�����
        {
            itemDetails = item;
            itemName.text = itemDetails.itemName;
            itemAmount.text = amount.ToString();
        }
        
        //��itemDetils�ÿգ�������һ����Ʒ����ʾ
        private void OnDestroy()
        {
            itemDetails = null;
        }

        //�ڱ�����λ�����ʱ����������Ʒ��itemDetails���󴫵ݸ�Display()����
        public void OnPointerDown(PointerEventData eventData)
        {
            EventHandler.CallChangeItemBarSelected(this);
            Selected = true;
            var color = image.color;
            color.a = 0.5f;                     //����image��͸��������Ϊ50%
            image.color = color;
            Debug.Log(itemDetails.itemName);
            itemDetailUI.Display(itemDetails);

        }

        //�������ͣ�ڱ�����λʱ����������ͼ����ʾ��͸���ȣ�ͻ����ʾ
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Selected)
            {
                var color = image.color;
                color.a = 0.8f;                 //����image��͸����Ϊ80%
                image.color = color;
            }
        }

        //�����Ӳ�λ���ƿ���ʱ�򴥷�������ͼ���͸���ȣ��ָ�������ʾ������
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Selected)
            {
                var color = image.color;
                color.a = 1f;                   //����image��͸����Ϊ100%������͸��
                image.color = color;
            }
        }
    }
}
