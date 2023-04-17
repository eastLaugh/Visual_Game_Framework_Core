using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

namespace VGF.Inventory
{
    public class ItemBase : MonoBehaviour
    {
        public UnityEvent OnGet = new UnityEvent();
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject KeyboardTip;
        private Collider coll;
        private ItemDetails itemDetails;
        private bool approach = false;
        public int itemID;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider>();
            KeyboardTip.SetActive(false);
        }
        void Start()
        {
            if (itemID != 0) InitItemSprite();
        }
        private void Update()
        {
            if (approach)
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

        public void InitItemSprite()
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemWorldSprite == null ? itemDetails.itemIcon : itemDetails.itemWorldSprite;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                approach = true;
                KeyboardTip.SetActive(true);
            }
        }
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


