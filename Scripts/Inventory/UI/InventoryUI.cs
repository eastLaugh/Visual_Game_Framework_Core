using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF.Inventory;

public class InventoryUI : MonoBehaviour
{
    //[SerializeField] private SlotUI[] playerSlots;
    public ItemDetailUI itemDetailUI;
    public SlotUI slotPrefab;
    public Transform RuleItemRoot;
    private SlotUI currentSelectedSlot;
    public GameObject InventoryPanel;
    private bool isOpen = false;
    public Button btnOpen;
    public Button btnClose;
    private void OnEnable()
    {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected += OnBarSelectedChange;
        
    }
    private void OnDisable()
    {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected -= OnBarSelectedChange;
        currentSelectedSlot = null;
    }
    void Start()
    {
        btnOpen.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                OpenInventoryUI();
            }
        });
        btnClose.onClick.AddListener(() =>
        {
            if (isOpen)
            {
                CloseInventoryUI();
            }
        });
        
    }
    private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        if(RuleItemRoot.childCount > 0)
        {
            for (int i = 0; i < RuleItemRoot.childCount; i++)
            {
                Destroy(RuleItemRoot.GetChild(i).gameObject);
            }
        }//TODO
        itemDetailUI.Clear();
        switch (location)
        {
            case InventoryLocation.player:
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].amount > 0)
                    {
                        var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        var slot = Instantiate(slotPrefab, RuleItemRoot);
                        slot.UpdateItemBar(item, list[i].amount);
                        slot.gameObject.SetActive(true);
                        
                    }
                }
                break;
        }
    }
    private void OnBarSelectedChange(SlotUI slotUI)
    {
        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.image.color = new Color(1, 1, 1, 1);
            currentSelectedSlot.Selected = false;
        }
        currentSelectedSlot = slotUI;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (!isOpen)
            {
                OpenInventoryUI();
            }
            else
            {
                CloseInventoryUI();
            }            
        }

    }
    void OpenInventoryUI()
    {
        InventoryPanel.SetActive(true);
        isOpen = true;
    }
    void CloseInventoryUI()
    {
        InventoryPanel.SetActive(false);
        isOpen = false;
    }
    
}
