using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//自定义VGF.UI库，增加控制场景的字幕显示的功能
namespace VGF.UI
{
    //此类或者方法已过时，不建议使用
    [Obsolete]

    //
    public class CaptionLoader : MonoBehaviour
    {

        private static CaptionLoader _instance;

        //获取CaptionLoader类的单例实例
        public static CaptionLoader instance
        {
            get
            {
                return _instance;
            }
        }

        //确保程序中只有一个CaptionLoader类的单例实例存在
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }
            _instance = this;

        }

        //定义队列存储字幕的信息，包括字母内容、显示时间、回调函数
        public Queue<CaptionPiece> captions = new Queue<CaptionPiece>();
        //定义当前正在播放的 CaptionPiece 对象
        public CaptionPiece currentCaptionPiece;
        public CaptionPiece[] test;
        //定义字幕的容器，用于显示字幕的UI元素
        public Image CaptionPanel;

        /// <summary>
        /// Caption是否正在放映
        /// </summary>
        public bool isPlaying;  //记录当前是否正在播放字幕

        //从队列中取出下一个字幕并显示在容器中，若队列中无字幕则记录当前不在播放字幕并不执行其他操作
        public void Next()
        {
            //判断当前是否有Caption
            if (captions.Count == 0)
            {
                isPlaying = false;
                return;
            }

            //激活面板
            CaptionPanel.gameObject.SetActive(true);
            isPlaying = true;

            //记录
            currentCaptionPiece = captions.Dequeue();
            CaptionPanel.GetComponentInChildren<Text>().text = currentCaptionPiece.content;
        }

        //添加新字幕对象到队列中，包括字母的内容、显示时间、回调函数
        public void Push(string content, float seconds = 0f, Action callback = null)
        {
            captions.Enqueue(new CaptionPiece { content = content, seconds = seconds, callback = callback });
            
            //如果当前不在播放字幕，则开始播放队列中的字幕
            if (!isPlaying)
            {
                Next();
            }
        }



        //逐帧更新调用，检测当前是否需要播放字幕
        private void Update()
        {
            test = captions.ToArray();

            if (!isPlaying)
            {
                Next();
            }
        }

        //停止当前正在播放的字幕，并清空队列，隐藏字幕容器
        public void Stop()
        {

            isPlaying = false;

            captions.Clear();

            CaptionLoader.instance.currentCaptionPiece = new CaptionPiece();
            CaptionAnimationEvent.instance.AnimationEnd();

            try
            {
                StopCoroutine(CaptionAnimationEvent.instance.currentCoroutine);
            }
            catch 
            {
                //可个性化编辑
            }

            CaptionAnimationEvent.instance.currentCoroutine = null;
            CaptionAnimationEvent.instance.gameObject.SetActive(false);

        }
    }

    [System.Serializable]
    //用于存储字幕的属性：文本内容、显示时间、回调函数
    public struct CaptionPiece
    {
        public string content;
        public float seconds;

        public Action callback;
    }
}
