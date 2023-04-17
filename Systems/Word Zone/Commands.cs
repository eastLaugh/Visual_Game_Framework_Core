using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace WordZone
{
    public sealed class Commands
    {
        public static void v(int v)
        {
            if (v > 0)
                Tone.Runtime.v = v;
            else
                Debug.LogWarning("v < 0");
        }

        public static void reset()
        {
            Tone.Runtime = new Tone();
        }

        public static IEnumerator halt(float time)
        {
            if(!Tone.Instant)
                yield return new WaitForSeconds(time);
        }


        // 实例 ： 什么？！[shake] 为什么![SHAKE] 会是这个样子！
        public static float duration = 0.5f;
        public static float strength = 1f;
        public static int vibrato = 10;
        public static float randomness = 90f;

        public static void Shake()
        {
            Debug.Log("Shake Command Invoked!");
            Camera.current.transform.DOShakePosition(duration, strength, vibrato, randomness);
        }



        public static void Wobble(){
            Tone.Runtime.Wobble=true;
        }

    }

}