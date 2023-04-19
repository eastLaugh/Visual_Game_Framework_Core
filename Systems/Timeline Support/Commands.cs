using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AutumnFramework;
using UnityEngine;

namespace VGF.Timeline
{

    [System.Serializable]
    public class Commands
    {
        #region  序列化

        [System.Serializable]
        public struct SerializedCommand
        {
            public string Command;
            public List<StringParamentOrObjectParamentOrFloatParament> paraments;

            [System.Serializable]
            public struct StringParamentOrObjectParamentOrFloatParament
            {
                public string StringParament;
                public float FloatParament;
                public Object ObjectParament;

            }
        }
        public List<SerializedCommand> SerializedCommands;

        public void 润()
        {
            foreach (SerializedCommand cmd in SerializedCommands)
            {
                MethodInfo methodInfo = typeof(Commands).GetMethod(cmd.Command, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (methodInfo == null)
                {
                    Debug.LogError($"找不到Command {cmd.Command} ，已略过");
                }
                else
                {
                    List<object> paraments = new();
                    foreach (SerializedCommand.StringParamentOrObjectParamentOrFloatParament parament in cmd.paraments)
                    {
                        if (parament.StringParament != null)
                        {
                            paraments.Add(parament.StringParament);
                        }
                        else if (parament.FloatParament != 0)
                        {
                            paraments.Add(parament.FloatParament);
                        }
                        else if (parament.ObjectParament != null)
                        {
                            paraments.Add(parament.ObjectParament);
                        }
                        else
                        {
                            Debug.LogError($"未提供参数 {cmd.Command}");
                        }
                    }

                    Debug.Log($"执行  {cmd.Command} ( {string.Join(" , ", paraments)} )");
                    try
                    {
                        methodInfo.Invoke(null, paraments.ToArray());
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
        }

        #endregion


        [Autowired]
        private static WordZone.WordZone wordZone;
        public static void Word(string text){
            wordZone.ParseAndEnque(text);
        }
    }
}