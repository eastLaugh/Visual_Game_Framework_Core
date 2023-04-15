using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;
using UnityEngine.SceneManagement;
namespace VGF.SL
{

    public class SavedHideStateController : MonoBehaviour
    {

        private void Start()
        {
            //Debug.Log("start");
            string sceneName=null;
            for (int i= 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name!="Persistent Scene")
                {
                    sceneName = SceneManager.GetSceneAt(i).name;
                    break;
                }
            }
            string objName=name;
            string savedKey =sceneName+objName;
            var data = Autumn.Harvest<ItemDisplayData>(); //之后可能会改
            if(data.HasSavedKey(savedKey))
            {
                Debug.Log("SetFalse");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("SetTrue");
            }

        }
        public void SaveHideState()
        {
            //Debug.Log("Saved");
            string sceneName = SceneManager.GetActiveScene().name;
            string objName = name;
            string savedKey = sceneName + objName;
            Autumn.Harvest<ItemDisplayData>().AddSavedKey(savedKey);//之后可能会改
        }
    }

}

