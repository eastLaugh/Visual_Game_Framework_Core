using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


//�������ڿ���ʱ����Ĳ��ֹ���
[System.Serializable]
public class CommandClip : PlayableAsset, ITimelineClipAsset
{
    public ClipCaps clipCaps => ClipCaps.None;
    public VGF.Timeline.Commands cmd;

    //����ʱ��������
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CommandBehaviour>.Create(graph);
        CommandBehaviour commandBehaviour = playable.GetBehaviour();
        commandBehaviour.cmd=cmd;
        
        return playable;
    }
}
