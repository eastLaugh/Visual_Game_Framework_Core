using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


//该类用于控制时间轴的部分功能
[System.Serializable]
public class CommandClip : PlayableAsset, ITimelineClipAsset
{
    public ClipCaps clipCaps => ClipCaps.None;
    public VGF.Timeline.Commands cmd;

    //播放时间轴内容
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CommandBehaviour>.Create(graph);
        CommandBehaviour commandBehaviour = playable.GetBehaviour();
        commandBehaviour.cmd=cmd;
        
        return playable;
    }
}
