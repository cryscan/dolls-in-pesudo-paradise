using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetMemoryCollectablePosition : Action
{
    public SharedGameObject subject;
    public SharedGameObject target;
    public SharedVector3 storedPosition;

    Memory memory;
    Collectable collectable;

    public override void OnStart()
    {
        memory = subject.Value.GetComponent<Memory>();
        collectable = target.Value.GetComponent<Collectable>();
    }

    public override TaskStatus OnUpdate()
    {
        Vector3 position;
        bool result = memory.GetCollectablePosition(collectable, out position);
        if (result)
        {
            storedPosition.Value = position;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}