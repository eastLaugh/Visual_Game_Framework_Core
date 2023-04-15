
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace VGF.SL
{
    public static class SaveSystem
    {
        /// <summary>
        /// ����SaveFile(�Ḳ��)
        /// </summary>
        public static void CreatSaveFile(string saveFileName,SaveData saveData)
        {
            var json = JsonUtility.ToJson(saveData);
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName); 
            
            try
            {
                File.WriteAllText(path, json);
                #if UNITY_EDITOR
                Debug.Log($"�ɹ��洢��{path}");
                #endif
            }
            catch(System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"ʧ�ܴ�����{path}.\n{exception}");
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
                Debug.LogError($"ʧ�ܶ�ȡ��{path}.\n{exception}");
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
                Debug.LogError($"ʧ��ɾ����{path}.\n{exception}");
                #endif
            }
        }
    }
}

