using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


//自定义WordZone库，包含了一些静态方法用于执行游戏中的一些命令
namespace WordZone
{
    [System.Serializable]
    public sealed class Commands
    {
        //设置声音的音量大小，当参数v大于0时生效
        public static void v(int v)
        {
            if (v > 0)
                Tone.Runtime.v = v;
            else
                Debug.LogWarning("v < 0");
        }

        //重置声音控制器
        public static void reset()
        {
            Tone.Runtime = new Tone();
        }

        //让程序等待指定的时间time后再继续执行
        public static IEnumerator halt(float time)
        {
            //如果Tone.Instant为true，即“瞬间模式”开启，那么不会等待
            if (!Tone.Instant)
                yield return new WaitForSeconds(time);
        }

        // 实例 ： 什么？！[shake] 为什么![SHAKE] 会是这个样子！
        public static float duration = 0.5f;
        public static float strength = 1f;
        public static int vibrato = 10;
        public static float randomness = 90f;

        //在当前场景中的相机上触发一个抖动效果（持续时间、强度、颤动次数和随机性）
        public static void Shake()
        {
            Debug.Log("Shake Command Invoked!");
            Camera.main.transform.DOShakePosition(duration, strength, vibrato, randomness);
        }

        //设置声音控制器的“摇摆模式”
        public static void Wobble()
        {
            Tone.Runtime.Wobble = true;
        }
    }
}
