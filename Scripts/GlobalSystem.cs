using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using AutumnFramework;
using VGF.Inventory;
using VGF.Plot;
using VGF.SL;


//定义VGF核心库，增加游戏的基本操作：开始新游戏、重置游戏数据、保存和加载游戏
namespace VGF
{
    public class GlobalSystem
    {
        [RuntimeInitializeOnLoadMethod]

        //初始化框架设置
        private static void Init()
        {
            Settings.Init();
            //Debug.Log(Settings.language);
        }

        //开始新游戏：清空玩家背包的物品，将当前游戏章节设置为0
        public static void NewGame()
        {
            //SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
            ResetGameData();
            EventHandler.CallNewGame();
            EventHandler.CallRunChapter(0);
        }
        
        //重设游戏数据
        public static void ResetGameData()
        {
            InventoryManager.Instance.EmptyItems();
            DataCollection.playerHP = 100;
            DataCollection.playerMaxHP = 100;
        }
        
        //保存游戏进度到磁盘
        public static void SaveGame()
        {
            var saveData = new SaveData();
            saveData.PlayerBagData = InventoryManager.Instance.playerBag_SO.itemList;
            saveData.ChapterIndex = PlotManager.Instance.currentIndex;
            saveData.ItemDisplayData = Autumn.Harvest<ItemDisplayData>().Save();
            SaveSystem.CreatSaveFile("PlayerData.Sav", saveData);
        }

        //从磁盘上读取游戏进度并恢复游戏状态（读档继续游戏），更新UI
        public static void LoadGame()
        {
            var saveData = SaveSystem.LoadSaveFile<SaveData>("PlayerData.Sav");
            var saveSystem = Autumn.Harvest<ItemDisplayData>();
            saveSystem.load(saveData.ItemDisplayData);
            //SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
            EventHandler.CallRunChapter(saveData.ChapterIndex);
            InventoryManager.Instance.playerBag_SO.itemList = saveData.PlayerBagData;
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, saveData.PlayerBagData);
        }
    }
}
