using UnityEngine;
using System.Collections.Generic;
using AutumnFramework;


[CreateAssetMenu(fileName = "SoundCollection", menuName = "Visual Game Framework/SoundCollection", order = 0)]
[Config]
//保存所有声音的详细信息
public class SoundCollection : ScriptableObject
{
    public List<SoundDetails> soundDetails;

    //查找指定名称的声音详细信息
    public SoundDetails FindSoundDetailsByName(string name)
    {
        return soundDetails.Find(i => i.SoundName == name);
    }
}
