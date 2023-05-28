using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    //播放音频
    public AudioSource audioSource;
    //缓存音频
    private Dictionary<string, AudioClip> dictAudio;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        dictAudio = new Dictionary<string, AudioClip>();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    //辅助函数：加载音频，需要确保音频文件的下载路径在Resources文件夹下
    private AudioClip LoadAudio(string path)
    {
        return (AudioClip)Resources.Load(path);
    }
    //辅助函数：获取音频，并且将其缓存在dicAudio中，避免重复加载
    private AudioClip GetAudio(string path)
    {
        if (!dictAudio.ContainsKey(path))
        {
            dictAudio[path] = LoadAudio(path);
        }
        return dictAudio[path];
    }
    public void PlayBGM(string name, float volume = 1.0f)
    {
        audioSource.Stop();
        audioSource.clip = GetAudio(name);
        audioSource.Play();
        this.audioSource.volume = volume;
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }
    //播放音效
    public void PlaySound(string path, float volume = 1.0f)
    {
        //PlayOneShot可以叠加播放
        this.audioSource.PlayOneShot(LoadAudio(path), volume);
        this.audioSource.volume = volume;
    }
    public void PlaySound(AudioSource audioSource, string path, float volume = 1.0f)
    {
        audioSource.PlayOneShot(LoadAudio(path), volume);
        audioSource.volume = volume;
    }
}
