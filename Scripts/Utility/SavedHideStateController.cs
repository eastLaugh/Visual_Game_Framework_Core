using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;
using UnityEngine.SceneManagement;


//自定义VGF.SL库，增加控制场景中游戏物体隐藏状态的功能
namespace VGF.SL
{
    //控制物品隐藏
    public class SavedHideStateController : MonoBehaviour
    {
        private void Start()
        {
            //Debug.Log("start");
            string sceneName = null;

            //检测该游戏物体在之前是否被保存过
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                //查找其他场景
                if (SceneManager.GetSceneAt(i).name != "Persistent Scene")
                {
                    sceneName = SceneManager.GetSceneAt(i).name;
                    break;
                }
            }

            string objName = name;
            string savedKey = sceneName + objName;
            var data = Autumn.Harvest<ItemDisplayData>();               //之后可能会改

            //如果保存过，则将该物体设置为隐藏状态
            if (data.HasSavedKey(savedKey))
            {
                Debug.Log("SetFalse");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("SetTrue");
            }
        }

        //保存游戏物品的隐藏状态
        public void SaveHideState()
        {
            //Debug.Log("Saved");
            string sceneName = SceneManager.GetActiveScene().name;
            string objName = name;
            string savedKey = sceneName + objName;
            Autumn.Harvest<ItemDisplayData>().AddSavedKey(savedKey);    //之后可能会改
        }
    }
}
