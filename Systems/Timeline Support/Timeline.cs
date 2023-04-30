using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.Linq;


//������ʱ����������Resources�ļ����е�TimelineAsset
public class Timeline : MonoBehaviour
{
    public static Dictionary<string, TimelineAsset> timelines;

    //������ʱͨ�����������úͼ��ͬ��Timeline
    void Start()
    {
        timelines = Resources.LoadAll<TimelineAsset>("").ToDictionary(timeline => timeline.name);
    }

}
