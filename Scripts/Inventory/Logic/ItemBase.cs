using System.Collections;
using System.Collections.Generic;
using System.Transactions;          //���ڴ��������.NET������
using UnityEngine;
using UnityEngine.Events;


//�Զ��������ռ䣬�������ڿ�����Ʒ����Ϊ��������ģ��ϵͳ����
namespace VGF.Inventory
{
    //�̳�MonoBehaviour�Ļ��࣬ʹ������Ա�������Ϸ�����еĶ�����
    public class ItemBase : MonoBehaviour
    {
        //�ж�����Ƿ��ȡ��Ʒ
        public UnityEvent OnGet = new UnityEvent();
        //���ָ���Ʒ��ͼ��
        [SerializeField] private SpriteRenderer spriteRenderer;
        //��ʾ��Ļ��ʾ��Ұ���"E"������ȡ����Ʒ
        [SerializeField] private GameObject KeyboardTip;
        //�����Ʒ�������������ײ
        private Collider coll;
        //��ʾ����Ʒ����ϸ��Ϣ�������ƺ�ͼ��
        private ItemDetails itemDetails;
        //�����жϱ�ʾ����Ʒ����ϸ��Ϣ
        private bool approach = false;
        //���ڱ�ʾ����Ʒ��ID
        public int itemID;

        //�ڶ��󱻴���ʱ������,���ڳ�ʼ��spriteRenderer��coll��KeyboardTip����
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider>();
            KeyboardTip.SetActive(false);
        }

        //�ڶ����һ�α�����ʱ�����ã����ڳ�ʼ��ĳ��Ʒ��ͼ��
        void Start()
        {
            if (itemID != 0) InitItemSprite();
        }

        /*��ÿһ֡�����ã��������Ƿ�ӽ�����Ʒ��
        ���������Ұ���"E"�������Խ�����Ʒ��ӵ���ҵĿ����*/
        private void Update()
        {
            if (approach)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool flag = InventoryManager.Instance.AddItem(itemID, 1);
                    if (flag)
                    {
                        gameObject.SetActive(false);
                        OnGet?.Invoke();
                    }
                }
            }
        }

        //��ʼ������Ʒ��ͼ�񣬻�ȡ����Ʒ����ϸ��Ϣ��������ͼ������spriteRenderer
        public void InitItemSprite()
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemWorldSprite == null ? itemDetails.itemIcon : itemDetails.itemWorldSprite;
            }
        }

        /*��������봥����ʱ���¼��ᱻ���ã��������Ƿ�Ϊ"Player"��
        ��������"approach"Ϊtrue������KeyboardTip�����ж�*/
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                approach = true;
                KeyboardTip.SetActive(true);
            }
        }

        /*�������뿪������ʱ���¼��ᱻ���ã��������Ƿ�Ϊ"Player"��
        ��������"approach"Ϊfalse������KeyboardTipֹͣ�ж�*/
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                approach = false;
                KeyboardTip.SetActive(false);
            }
        }
    }
}