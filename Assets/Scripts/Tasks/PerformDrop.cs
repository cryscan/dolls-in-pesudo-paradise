using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PerformDrop : Action
{
    public SharedGameObject subject;

    Holder holder;

    public override void OnStart()
    {
        holder = subject.Value.GetComponent<Holder>();
    }

    public override TaskStatus OnUpdate()
    {
        holder.Drop();
        return TaskStatus.Success;
    }
}