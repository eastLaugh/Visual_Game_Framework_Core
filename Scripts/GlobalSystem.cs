using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using AutumnFramework;
using VGF.Inventory;
using VGF.Plot;
using VGF.SL;


//����VGF���Ŀ⣬������Ϸ�Ļ�����������ʼ����Ϸ��������Ϸ���ݡ�����ͼ�����Ϸ
namespace VGF
{
    public class GlobalSystem
    {
        [RuntimeInitializeOnLoadMethod]

        //��ʼ���������
        private static void Init()
        {
            Settings.Init();
            //Debug.Log(Settings.language);
        }

        //��ʼ����Ϸ�������ұ�������Ʒ������ǰ��Ϸ�½�����Ϊ0
        public static void NewGame()
        {
            //SceneManager.LoadScene("Persistent Scene", LoadSceneMode.Single);
            ResetGameData();
            EventHandler.CallNewGame();
            EventHandler.CallRunChapter(0);
        }
        
        //������Ϸ����
        public static void ResetGameData()
        {
            InventoryManager.Instance.EmptyItems();
            DataCollection.playerHP = 100;
            DataCollection.playerMaxHP = 100;
        }
        
        //������Ϸ���ȵ�����
        public static void SaveGame()
        {
            var saveData = new SaveData();
            saveData.PlayerBagData = InventoryManager.Instance.playerBag_SO.itemList;
            saveData.ChapterIndex = PlotManager.Instance.currentIndex;
            saveData.ItemDisplayData = Autumn.Harvest<ItemDisplayData>().Save();
            SaveSystem.CreatSaveFile("PlayerData.Sav", saveData);
        }

        //�Ӵ����϶�ȡ��Ϸ���Ȳ��ָ���Ϸ״̬������������Ϸ��������UI
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
