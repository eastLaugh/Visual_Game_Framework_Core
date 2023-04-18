using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF.Inventory;                //�����Զ���ı���ϵͳ�������ռ䣩


//ʵ������Ϸ�д򿪺͹رձ�����UI���桢���±�����UI���桢ѡ�б����е���Ʒ�ȹ���
public class InventoryUI : MonoBehaviour
{
    //[SerializeField] private SlotUI[] playerSlots;
    public ItemDetailUI itemDetailUI;
    public SlotUI slotPrefab;               //��ű������ӵ�UIԤ����
    public Transform RuleItemRoot;          //��ű������ӵĸ�����
    private SlotUI currentSelectedSlot;
    public GameObject InventoryPanel;
    private bool isOpen = false;
    public Button btnOpen;
    public Button btnClose;

    //����������ʱ����������
    private void OnEnable()
    {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected += OnBarSelectedChange;
    }

    //���������ر�ʱ���Ƴ�����
    private void OnDisable()
    {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        EventHandler.ChangeItemBarSelected -= OnBarSelectedChange;
        currentSelectedSlot = null;
    }

    //ͨ����Ӽ�����ť�ĵ���¼��ķ�ʽ��ʵ����ͨ����ť�򿪺͹رձ���UI����Ĺ���
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
    
    //����location����������λ�ã���list����Ʒ�б���ʵʱ���±�����UI����
    private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        //��ʱ������б����ڵ���Ʒ
        if (RuleItemRoot.childCount > 0)
        {
            for (int i = 0; i < RuleItemRoot.childCount; i++)
            {
                Destroy(RuleItemRoot.GetChild(i).gameObject);
            }
        }
        itemDetailUI.Clear();

        //ʵ����SlotUI������������
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
    
    //ͨ������slotUI��ѡ�е�ǰ��SlotUI������SlotUI��ͼ���Ϊ��ɫ
    private void OnBarSelectedChange(SlotUI slotUI)
    {
        if (currentSelectedSlot != null)
        {
            /*����ͨ���޸�Color(Transparency, R, G, B)��ֵ�ﵽ��ͬ����ʾЧ����
            ��������a��alpha������Transparency��͸���ȣ�*/
            currentSelectedSlot.image.color = new Color(1, 1, 1, 1);    
            currentSelectedSlot.Selected = false;
        }
        currentSelectedSlot = slotUI;
    }

    //��֡���ã�����û��Ƿ���"B"������/�رձ���
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
    
    //�����򿪵ļ��
    void OpenInventoryUI()
    {
        InventoryPanel.SetActive(true);
        isOpen = true;
    }
    
    //�����رյļ��
    void CloseInventoryUI()
    {
        InventoryPanel.SetActive(false);
        isOpen = false;
    }
}
