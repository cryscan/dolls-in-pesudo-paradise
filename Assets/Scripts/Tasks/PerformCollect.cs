using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PerformCollect : Action
{
    public SharedGameObject subject;
    public SharedGameObject target;

    Holder holder;
    Interactable interactable;

    public override void OnStart()
    {
        holder = subject.Value.GetComponent<Holder>();
        interactable = target.Value.GetComponent<Interactable>();
    }

    public override TaskStatus OnUpdate()
    {
        holder.Collect(interactable);
        return TaskStatus.Success;
    }
}