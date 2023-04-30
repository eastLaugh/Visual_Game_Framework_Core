using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


//该类用于部分控制角色的行为
public class CommandBehaviour : PlayableBehaviour
{
    public VGF.Timeline.Commands cmd;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if (Application.isPlaying)
            cmd.润();
    }
}
