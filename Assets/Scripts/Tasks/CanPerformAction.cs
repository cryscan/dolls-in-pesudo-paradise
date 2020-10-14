using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanPerformAction : Conditional
{
    public SharedGameObject subject;
    public SharedGameObject target;
    public ActionType action;

    InteractDetector detector;
    Interactable interactable;

    public override void OnStart()
    {
        detector = subject.Value.GetComponent<InteractDetector>();
        interactable = target.Value.GetComponent<Interactable>();
    }

    public override TaskStatus OnUpdate()
    {
        if (detector.GetActions(interactable).Contains(action)) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}
