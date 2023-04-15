using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using System.Linq;
using Unity;
[Bean]
public class AudioCenter:MonoBehaviour
{

    [SerializeField]
    [Autowired]
    private SoundCollection soundCollection;
    public void Play(string name)
    {
        var audioClip= soundCollection.FindSoundDetailsByName(name).SoundClip;

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip=audioClip;
        audioSource.Play();
    }
}
