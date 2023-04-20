using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using System.Linq;
using Unity;


[Bean]
//实现单例音频管理器
public class AudioCenter:MonoBehaviour
{

    [SerializeField]
    [Autowired]
    private SoundCollection soundCollection;

    //根据传入的音频名称播放相应的音频
    public void Play(string name)
    {
        var audioClip= soundCollection.FindSoundDetailsByName(name).SoundClip;

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip=audioClip;
        audioSource.Play();
    }
}
