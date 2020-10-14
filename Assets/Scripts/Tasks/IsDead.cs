using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : Conditional
{
    public Death death;

    public override TaskStatus OnUpdate()
    {
        if (death.dead) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}