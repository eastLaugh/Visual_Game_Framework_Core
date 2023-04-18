using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//自定义VGF.SL库，添加游戏的存档、读档功能，方便数据的保存和读取
namespace VGF.SL
{
    //存档系统，用于游戏数据的保存和加载
    public static class SaveSystem
    {
        /// <summary>
        /// 生成SaveFile(会覆盖)
        /// </summary>
        //根据指定的名称和要保存的数据，创建保存文件
        public static void CreatSaveFile(string saveFileName,SaveData saveData)     
        {
            //将数据转化为json格式的字符串
            var json = JsonUtility.ToJson(saveData);
            //获取保存文件的路径
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName); 
            
            //写入成功或失败时候控制台的输出
            try
            {
                //将json字符串写入保存文件中
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

        //通过传入保存文件的名称来读取指定的保存的文件
        public static T LoadSaveFile<T>(string saveFileName)
        {
            //获取保存的文件的路径
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                //读取保存的文件中的数据
                var json = File.ReadAllText(path);
                //将json格式的字符串转化为指定类型的对象data
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception exception)
            {
                //读取失败时控制台的输出
                #if UNITY_EDITOR
                Debug.LogError($"失败读取至{path}.\n{exception}");
                #endif
                return default(T);
            }
        }

        //通过传入保存文件的名称来删除保存的文件
        public static void DeleteSaveFile(string saveFileName)
        {
            //获取保存的文件的路径
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
            
            try
            {
                //删除该路径下的文件
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                //删除失败时控制台的输出
                #if UNITY_EDITOR
                Debug.LogError($"失败删除至{path}.\n{exception}");
                #endif
            }
        }
    }
}
