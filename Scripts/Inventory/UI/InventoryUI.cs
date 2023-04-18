using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF.Inventory;                //引用自定义的背包系统（命名空间）


//实现在游戏中打开和关闭背包的UI界面、更新背包的UI界面、选中背包中的物品等功能
public class InventoryUI : MonoBehaviour
{
    //[SerializeField] private SlotUI[] playerSlots;
    public ItemDetailUI itemDetailUI;
    public SlotUI slotPrefab;               //存放背包格子的UI预制体
    public Transform RuleItemRoot;          //存放背包格子的父物体
    private SlotUI currentSelectedSlot;
    public GameObject InventoryPanel;
    private bool isOpen = false;
    public Button btnOpen;
    public Button btnClose;

    //当背包被打开时，触发监听
    private void OnEnable()
    {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected += OnBarSelectedChange;
    }

    //当背包被关闭时，移除监听
    private void OnDisable()
    {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected -= OnBarSelectedChange;
        currentSelectedSlot = null;
    }

    //通过添加监听按钮的点击事件的方式，实现了通过按钮打开和关闭背包UI界面的功能
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
    
    //传入location（背包所在位置）和list（物品列表），实时更新背包的UI界面
    private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        //暂时清除所有背包内的物品
        if (RuleItemRoot.childCount > 0)
        {
            for (int i = 0; i < RuleItemRoot.childCount; i++)
            {
                Destroy(RuleItemRoot.GetChild(i).gameObject);
            }
        }
        itemDetailUI.Clear();

        //实例化SlotUI并更新其内容
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
    
    //通过传入slotUI来选中当前的SlotUI，并将SlotUI的图像改为灰色
    private void OnBarSelectedChange(SlotUI slotUI)
    {
        if (currentSelectedSlot != null)
        {
            /*可以通过修改Color(Transparency, R, G, B)的值达到不同的显示效果，
            代码中用a（alpha）代替Transparency（透明度）*/
            currentSelectedSlot.image.color = new Color(1, 1, 1, 1);    
            currentSelectedSlot.Selected = false;
        }
        currentSelectedSlot = slotUI;
    }

    //逐帧调用，检测用户是否按下"B"键来打开/关闭背包
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
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
    
    //背包打开的监测
    void OpenInventoryUI()
    {
        InventoryPanel.SetActive(true);
        isOpen = true;
    }
    
    //背包关闭的监测
    void CloseInventoryUI()
    {
        InventoryPanel.SetActive(false);
        isOpen = false;
    }
}
