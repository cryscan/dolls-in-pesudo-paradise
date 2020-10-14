using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetFigureAnimationBlend : Action
{
    public SharedGameObject subject;
    public float blend;
    FigureAnimation animation;

    public override void OnStart()
    {
        animation = subject.Value.GetComponent<FigureAnimation>();
    }

    public override TaskStatus OnUpdate()
    {
        animation.SetTargetBlend(blend);
        return TaskStatus.Success;
    }
}
