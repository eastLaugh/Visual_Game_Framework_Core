
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace VGF.SL
{
    public static class SaveSystem
    {
        /// <summary>
        /// 生成SaveFile(会覆盖)
        /// </summary>
        public static void CreatSaveFile(string saveFileName,SaveData saveData)
        {
            var json = JsonUtility.ToJson(saveData);
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName); 
            
            try
            {
                File.WriteAllText(path, json);
                #if UNITY_EDITOR
                Debug.Log($"成功存储至{path}");
                #endif
            }
            catch(System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"失败储存至{path}.\n{exception}");
                #endif
            }
        }
        public static T LoadSaveFile<T>(string saveFileName)
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"失败读取至{path}.\n{exception}");
                #endif
                return default(T);
            }
        }
        public static void DeleteSaveFile(string saveFileName)
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                File.Delete(path);
            }
            catch(System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"失败删除至{path}.\n{exception}");
                #endif
            }
        }
    }
}

