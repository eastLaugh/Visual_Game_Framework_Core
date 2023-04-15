using UnityEngine.SceneManagement;
using VGF.Inventory;
using VGF.Plot;
using VGF.SL;
using AutumnFramework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace VGF
{
    public class GlobalSystem
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Settings.Init();
            //Debug.Log(Settings.language);
        }
        public static void NewGame()
        {
            
            //SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
            InventoryManager.Instance.EmptyItems();
            Player.instance.Mute = true;
            EventHandler.CallRunChapter(0);
        }
        public static void ResetGameData()
        {
            
        }
        public static void SaveGame()
        {
            var saveData = new SaveData();
            saveData.PlayerBagData = InventoryManager.Instance.playerBag_SO.itemList;
            saveData.ChapterIndex = PlotManager.Instance.currentIndex;
            saveData.ItemDisplayData = Autumn.Harvest<ItemDisplayData>().Save();
            SaveSystem.CreatSaveFile("PlayerData.Sav", saveData);
        }
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


