using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.Linq;
public class Timeline : MonoBehaviour
{
    public static Dictionary<string,TimelineAsset> timelines;

    void Start()
    {
        timelines = Resources.LoadAll<TimelineAsset>("").ToDictionary(timeline=>timeline.name);

    }

}
