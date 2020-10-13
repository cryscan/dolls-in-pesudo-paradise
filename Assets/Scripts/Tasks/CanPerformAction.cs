using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanPerformAction : Conditional
{
    public ActionType action;
    public InteractDetector subject;
    public Interactable _object;

    public override TaskStatus OnUpdate()
    {
        if (subject.GetActions(_object).Contains(action)) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}
