using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetFigureAnimationBlend : Action
{
    public FigureAnimation animation;
    public float blend;

    public override void OnAwake()
    {
        if (animation == null) animation = GetComponent<FigureAnimation>();
    }

    public override TaskStatus OnUpdate()
    {
        animation.SetTargetBlend(blend);
        return TaskStatus.Success;
    }
}
