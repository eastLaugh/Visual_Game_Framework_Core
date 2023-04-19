using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;        //便于调试视觉脚本
using UnityEngine;
using UnityEngine.UI;


//自定义VGF.UI，加入了实现能向玩家提示加载信息的面板
namespace VGF.UI
{
    //继承了泛型类Singleton<HintLoader>
    public class HintLoader : Singleton<HintLoader>
    {
        [SerializeField] private Image hintImage;   //显示提示信息面板的背景图
        private Text hintText;                      //显示实际提示的信息
        private Animator hintAnimator;              //播放显示和隐藏提示信息面板时的动画效果

        //初始化各变量
        protected override void Awake()
        {
            base.Awake();
            hintImage.gameObject.SetActive(false);  //初始设置此信息面板的背景图为不可见
            hintText = hintImage.GetComponentInChildren<Text>();
            hintAnimator = hintImage.GetComponent<Animator>();
        }

        /// <summary>
        /// 打开Hint面板
        /// </summary>
        /// <param name="message">显示的信息</param>
        public void HintOn(string message)          //在提示信息面板上显示给定的文本消息
        {
            hintImage.gameObject.SetActive(true);   //使背景图可见
            hintText.text = message;
        }

        /// <summary>
        /// 关闭Hint面板
        /// </summary>
        public void HintOff()                       //会播放隐藏提示信息面板的动画
        {
            //hintText.text = string.Empty;
            hintAnimator.SetTrigger("Fade");
            //hintImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 使Hint面板显示一段时间(可打断)
        /// </summary>
        /// <param name="message">显示的信息</param>
        /// <param name="seconds">显示的时间</param>
        public void HintWithSeconds(string message, int seconds)    //在提示信息面板上显示一段时间的给定文本消息
        {
            hintImage.gameObject.SetActive(true);
            hintText.text = message;
            StartCoroutine(Wait(message, seconds));
        }

        //提供协程，等待指定的时间后自动关闭提示信息面板
        private IEnumerator Wait(string message, int seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (hintText.text == message)
                hintAnimator.SetTrigger("Fade");
            yield break;
        }
    }
}
