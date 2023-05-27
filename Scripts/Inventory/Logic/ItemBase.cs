using System.Collections;
using System.Collections.Generic;
using System.Transactions;          //用于处理事务的.NET框架组件
using UnityEngine;
using UnityEngine.Events;


//自定义命名空间，该类用于控制物品的行为，供其他模块系统引用
namespace VGF.Inventory
{
    //继承MonoBehaviour的基类，使该类可以被用于游戏场景中的对象里
    public class ItemBase : MonoBehaviour
    {
        //判断玩家是否获取物品
        public UnityEvent OnGet = new UnityEvent();
        //呈现该物品的图像
        [SerializeField] private SpriteRenderer spriteRenderer;
        //显示字幕提示玩家按下"E"键来获取该物品
        [SerializeField] private GameObject KeyboardTip;
        //检测物品与其他对象的碰撞
        private Collider coll;
        //显示该物品的详细信息，如名称和图标
        private ItemDetails itemDetails;
        //用于判断表示该物品的详细信息
        private bool approach = false;
        //用于表示该物品的ID
        public int itemID;

        //在对象被创建时被调用,用于初始化spriteRenderer、coll和KeyboardTip变量
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider>();
            KeyboardTip.SetActive(false);
        }

        //在对象第一次被启用时被调用，用于初始化某物品的图像
        void Start()
        {
            if (itemID != 0) InitItemSprite();
        }

        /*在每一帧被调用，检查玩家是否接近该物品，
        并且如果玩家按下"E"键，则尝试将该物品添加到玩家的库存中*/
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

        //初始化该物品的图像，获取该物品的详细信息，并将其图像分配给spriteRenderer
        public void InitItemSprite()
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemWorldSprite == null ? itemDetails.itemIcon : itemDetails.itemWorldSprite;
            }
        }

        /*当对象进入触发器时该事件会被调用，检测对象是否为"Player"，
        是则设置"approach"为true并激活KeyboardTip继续行动*/
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                approach = true;
                KeyboardTip.SetActive(true);
            }
        }

        /*当对象离开触发器时该事件会被调用，检测对象是否为"Player"，
        是则设置"approach"为false并激活KeyboardTip停止行动*/
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