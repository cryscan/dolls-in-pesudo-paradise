using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetMemoryCollectablePosition : Action
{
    public SharedGameObject subject;
    public SharedGameObject target;
    public SharedVector3 position;

    Memory memory;
    Collectable collectable;

    public override void OnStart()
    {
        memory = subject.Value.GetComponent<Memory>();
        collectable = target.Value.GetComponent<Collectable>();
    }

    public override TaskStatus OnUpdate()
    {
        memory.SetCollectablePosition(collectable, position.Value);
        return TaskStatus.Success;
    }
}