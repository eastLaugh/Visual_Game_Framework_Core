using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//�Զ���VGF.SL�⣬�����Ϸ�Ĵ浵���������ܣ��������ݵı���Ͷ�ȡ
namespace VGF.SL
{
    //�浵ϵͳ��������Ϸ���ݵı���ͼ���
    public static class SaveSystem
    {
        /// <summary>
        /// ����SaveFile(�Ḳ��)
        /// </summary>
        //����ָ�������ƺ�Ҫ��������ݣ����������ļ�
        public static void CreatSaveFile(string saveFileName,SaveData saveData)     
        {
            //������ת��Ϊjson��ʽ���ַ���
            var json = JsonUtility.ToJson(saveData);
            //��ȡ�����ļ���·��
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName); 
            
            //д��ɹ���ʧ��ʱ�����̨�����
            try
            {
                //��json�ַ���д�뱣���ļ���
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

        //ͨ�����뱣���ļ�����������ȡָ���ı�����ļ�
        public static T LoadSaveFile<T>(string saveFileName)
        {
            //��ȡ������ļ���·��
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                //��ȡ������ļ��е�����
                var json = File.ReadAllText(path);
                //��json��ʽ���ַ���ת��Ϊָ�����͵Ķ���data
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception exception)
            {
                //��ȡʧ��ʱ����̨�����
                #if UNITY_EDITOR
                Debug.LogError($"ʧ�ܶ�ȡ��{path}.\n{exception}");
                #endif
                return default(T);
            }
        }

        //ͨ�����뱣���ļ���������ɾ��������ļ�
        public static void DeleteSaveFile(string saveFileName)
        {
            //��ȡ������ļ���·��
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
            
            try
            {
                //ɾ����·���µ��ļ�
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                //ɾ��ʧ��ʱ����̨�����
                #if UNITY_EDITOR
                Debug.LogError($"ʧ��ɾ����{path}.\n{exception}");
                #endif
            }
        }
    }
}
