using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//设置音效的名称、音频剪辑、最小音调、最大音调和音量
public class SoundDetails
{
    public string SoundName;

    public AudioClip SoundClip;

    [Range(0.1f, 1.5f)]
    public float SoundPitchMin = 1f;
    [Range(0.1f, 1.5f)]
    public float SoundPitchMax = 1f;
    [Range(0f, 1f)]
    public float SoundVolume = 1f;

    /// <summary>
    /// 这是一个隐式转换  显示转换需要把implicit置换为explicit （可以在显示转换中直接套用隐式转换）
    /// </summary>
    /// <param name="obj"></param>
    public static implicit operator AudioClip(SoundDetails obj) 
    {
        //将SoundDetails对象隐式转换为AudioClip对象并返回
        return obj.SoundClip;
    }
}
