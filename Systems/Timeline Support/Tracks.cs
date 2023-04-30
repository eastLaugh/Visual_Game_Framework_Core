using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


//设置轨道的颜色
[TrackColor(0.4f, 0.8f, 0.4f)]
//指定剪辑类型
[TrackClipType(typeof(CommandClip))]
// [TrackBindingType(typeof(GameObject))]
public class VisualGameFrameworkCommandsTrack : TrackAsset
{
    //
}