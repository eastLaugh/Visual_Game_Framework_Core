using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.Linq;


//在启动时加载所有在Resources文件夹中的TimelineAsset
public class Timeline : MonoBehaviour
{
    public static Dictionary<string, TimelineAsset> timelines;

    //在运行时通过名称来引用和激活不同的Timeline
    void Start()
    {
        timelines = Resources.LoadAll<TimelineAsset>("").ToDictionary(timeline => timeline.name);
    }

}
