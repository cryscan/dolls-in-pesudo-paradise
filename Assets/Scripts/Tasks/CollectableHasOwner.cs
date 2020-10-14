using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CollectableHasOwner : Conditional
{
    public SharedGameObject collectableGameObject;
    Collectable collectable;

    public override void OnStart()
    {
        collectable = collectableGameObject.Value.GetComponent<Collectable>();
    }

    public override TaskStatus OnUpdate()
    {
        if (collectable.holder != null) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}