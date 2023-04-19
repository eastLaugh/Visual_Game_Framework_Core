using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CommandBehaviour : PlayableBehaviour
{
    public VGF.Timeline.Commands cmd;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if(Application.isPlaying)
            cmd.润();
    }

}
