// 引用命名空间实现在Unity编辑器环境下创建和编辑UI元素以及进行脚本的编写和编辑
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

public class InventoryEditor : EditorWindow
{
    // 获取VisualElement
    private ItemDataList_SO database;
    private List<ItemDetails> itemList = new List<ItemDetails>();
    private VisualTreeAsset itemRowTemplate;
    

    private ListView itemListView;
    /// <summary>
    /// 画面右边的ScrollView
    /// </summary>
    private ScrollView itemDetailsSection;
    private Sprite defaultIcon;
    private ItemDetails activeItem;
    private VisualElement iconPreview;


    // 弹出一个包含一个名为“InventoryEditor”选项的下拉菜单
    [MenuItem("Editor/InventoryEditor")]
    
    
    // 在Unity编辑器中创建一个标题为"InventoryEditor"的窗口
    public static void ShowExample()
    {
        InventoryEditor wnd = GetWindow<InventoryEditor>();
        wnd.titleContent = new GUIContent("InventoryEditor");
    }
    

    public void CreateGUI()
    {
        // 每个编辑器窗口都包含一个根VisualElement对象
        VisualElement root = rootVisualElement;

        // VisualElement对象可以包含遵循树层次结构的其他VisualElement。
        
        /*VisualElement label = new Label("Hello World! From C#");
        root.Add(label);*/

        // 引入UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Inventory System/InventoryEditor.uxml");
        VisualElement mainTemplate = visualTree.Instantiate();
        root.Add(mainTemplate);
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Inventory System/ItemRowTemplate.uxml");
        // 拿到默认Icon图片
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Unity Store Resouces/M Studio/Art/Items/Icons/icon_M.png");
        // 变量赋值
        itemListView = root.Q<VisualElement>("BackGround").Q<ListView>("ItemList");
        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPreview = itemDetailsSection.Q<VisualElement>("IconView");
        // 获得按键
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteClicked;
        // 加载数据
        LoadDataBase();
        // 生成ListView
        GenerateListView();

        // 样式表可以添加到VisualElement中。
        // 该样式将应用于VisualElement及其所有子元素。

        /*var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Inventory System/InventoryEditor.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);*/
    }
    #region 按键事件


    // 在物品清单编辑器中删除选中的物品
    private void OnDeleteClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();
        itemDetailsSection.visible = false;
    }


    // 在物品清单编辑器中加入选中的物品，包括名称、ID和图标等信息，并刷新物品列表的显示
    private void OnAddItemClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemName = "NEW ITEM";
        newItem.itemID = 1000 + itemList.Count;
        newItem.itemIcon = defaultIcon;
        itemList.Add(newItem);
        itemListView.Rebuild();
    }
    #endregion


    // 加载数据，列表包含所有物品的详细信息，检查物品列表中第一个物品的ID避免卡bug
    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            database = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }
        itemList = database.itemDataList;
        //如果不标记则无法记录数据
        EditorUtility.SetDirty(database);
        //Debug.Log(itemList[0].itemID);
    }


    // 生成一个UI元素列表视图，其中包括了每个列表项的图标和名称，代码将隐藏右侧的信息面板中
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("List_Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                e.Q<Label>("List_Name").text = itemList[i] == null ? "NO ITEM" : itemList[i].itemName;
            }
        };
        //Debug.Log(itemList[0].itemID);
        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;
        itemListView.onSelectionChange += OnlistSelectiontChange;
        //右侧信息面板不可见
        itemDetailsSection.visible = false;
    }


    //实现当用户在物品列表中选中一个物品时，右侧会显示该物品的详细信息，并在右侧面板中显示
    private void OnlistSelectiontChange(IEnumerable<object> selectedItem)
    {
        activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        itemDetailsSection.visible = true;
    }


    //在编辑器中显示和编辑当前活动的物品的详细信息
    private void GetItemDetails()
    {
        //创建和更新一个信息面板，该面板显示物品的各种属性，包括ID、名称、图标
        itemDetailsSection.MarkDirtyRepaint(); 
        itemDetailsSection.Q<IntegerField>("itemID").value = activeItem.itemID;
        itemDetailsSection.Q<IntegerField>("itemID").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemID = evt.newValue;
        });
        itemDetailsSection.Q<TextField>("itemName").value = activeItem.itemName;
        itemDetailsSection.Q<TextField>("itemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        itemDetailsSection.Q<ObjectField>("itemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("itemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;
            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });

        //其他变量绑定：物品类型和描述
        itemDetailsSection.Q<ObjectField>("itemWorldSprite").value = activeItem.itemWorldSprite;
        itemDetailsSection.Q<ObjectField>("itemWorldSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemWorldSprite = (Sprite)evt.newValue;
        });
        itemDetailsSection.Q<EnumField>("itemType").Init(activeItem.itemType);
        itemDetailsSection.Q<EnumField>("itemType").value = activeItem.itemType;
        itemDetailsSection.Q<EnumField>("itemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });
        itemDetailsSection.Q<TextField>("Description").value = activeItem.Description;
        itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.Description = evt.newValue;
        });

        
        
        /*itemDetailsSection.Q<IntegerField>("itemUseRadius").value = activeItem.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("itemUseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanPickedup").value = activeItem.canPickedup;
        itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("Price").value = activeItem.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
        });

        itemDetailsSection.Q<Slider>("SellPercentage").value = activeItem.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
        });*/
    }
}
