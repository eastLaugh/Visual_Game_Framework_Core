using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;


//自定义VGF.UI库，增加字幕动画播放的功能
namespace VGF.UI
{
    //该单例继承了MonoBehaviour基类，用于协调多个字幕动画的播放
    public class CaptionAnimationEvent : MonoBehaviour
    {

        private static CaptionAnimationEvent _instance;     //保存类的单例实例

        
        //获取单例实例
        public static CaptionAnimationEvent instance
        {
            get
            {
                return _instance;
            }
        }
        //public GameObject RequireClosing;
        private float seconds => CaptionLoader.instance.currentCaptionPiece.seconds;        //获取当前字幕片段的持续时间
        private Action callback => CaptionLoader.instance.currentCaptionPiece.callback;     //获取当前字幕的回调方法
        private Animator animator => GetComponent<Animator>();                              //获取设置的动画组件

        public Coroutine currentCoroutine;                                                  //保存协程对象

        private void Awake()
        {
            //存在单例实例，则销毁该单例实例，并将当前实例赋值给_instance
            if (_instance != null)
            { 
                Destroy(_instance); 
            }
            _instance = this;

            //设置gameObject为不活跃状态，等待下次播放字幕时的激活
            gameObject.SetActive(false);
        }

        /*在字幕播放完后触发，可以用于通知CaptionLoader当前片段已经播放完毕，
        并且根据当前片段的回调方法执行对应的操作*/
        public void AnimationEnd()
        {
            //设置gameObject为不活跃状态
            gameObject.SetActive(false);
            //提示当前字幕片段已经播放完成
            CaptionLoader.instance.isPlaying = false;

            //执行CaptionLoader的回调
            if (callback != null)
            {
                callback?.Invoke();
            }

            //执行NEXT方法，用于播放下一个字幕
            /*这个NEXT必须在回调之后，不然会先Next，导致currentCaptionPiece被修改，callback执行的是下一个piece的回调*/
            CaptionLoader.instance.currentCaptionPiece = new CaptionPiece();
            CaptionLoader.instance.Next();
        }
        
        //等待指定的时间（s），然后执行传入的委托
        private IEnumerator Wait(float seconds, System.Action action)
        {
            //使用WaitForSecondsRealtime而不是WaitForSeconds，避免在游戏暂停的情况下仍然继续等待时间
            yield return new WaitForSecondsRealtime(seconds);
            action();
        }

        //在字幕播放的时候触发，用于设置协程的持续时间，并在持续时间结束后启动协程
        void AnimationStay()
        {
            //创建并启动协程播放动画，在一定时间后将动画的状态设置为"Finish"
            currentCoroutine = StartCoroutine(Wait(seconds, () =>
            {
                animator.SetTrigger("Finish");
            }));
        }
    }
}
