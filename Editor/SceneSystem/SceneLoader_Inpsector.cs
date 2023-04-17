using UnityEngine;
using UnityEditor;
using VGF.SceneSystem;              //引入我们自定义的命名空间，实现场景切换等功能
using UnityEngine.SceneManagement;  //引入Unity官方的场景管理API，实现场景加载、激活等功能


//在调用此脚本中内容时，使用"SceneLoader"的类来覆盖默认的"Inspector"界面
[CustomEditor(typeof(SceneLoader))]


//继承Editor类，用于自定义SceneLoader的Inpsector面板中的显示和行为
public class SceneLoader_Inpsector : Editor
{
    //增加一个注释
    //Scene[] scenes;
    string originSceneName;

    //可继续开发用于其他使能交互
    private void OnEnable()
    {
        // scenes= new Scene[SceneManager.sceneCountInBuildSettings];
        // for(int i=0;i<SceneManager.sceneCountInBuildSettings;i++){
        //     scenes[i] = SceneManager.GetSceneByBuildIndex(i);
        // }

    }

    //在Inspector面板上显示所有基类元素的内容
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // 可继续开发
        // foreach(var scene in scenes
        // {
        //     if(GUILayout.Button(scene.name))
        //     {
        //         if(Application.isPlaying)
        //         {
        //             originSceneName = SceneManager.GetSceneAt(1).name;
        //             VGF.Plot.ChapterBase.Instance.SceneMoveThen(scene.name,action:()=>
        //             {
        //                 Debug.LogFormat("<color=green>手动切换地图成功 场景为:{0}→{1} 坐标是默认点",originSceneName,scene.name);
        //             });
        //         }
        //         else
        //         {
        //             SceneLoader.instance.SwitchSceneByName(scene.name);
        //         }
        //     }
        // }
        // for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        // {
        //     var scene = SceneManager.GetSceneByBuildIndex(i);
        //     if (GUILayout.Button(scene.path))
        //     {
        //         if (Application.isPlaying)
        //         {
        //             originSceneName = SceneManager.GetSceneAt(1).name;
        //             VGF.Plot.ChapterBase.Instance.SceneMoveThen(scene.name, action: () =>
        //             {
        //                 Debug.LogFormat("<color=green>手动切换地图成功 场景为:{0}→{1} 坐标是默认点", originSceneName, scene.name);
        //             });
        //         }
        //         else
        //         {
        //             SceneLoader.instance.SwitchSceneByName(scene.name);
        //         }
        //     }
        // }
    }
}