using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetPlayerMove : Action
{
    public PlayerMovement movement;
    public bool enabled;

    public override TaskStatus OnUpdate()
    {
        movement.enabled = enabled;
        return TaskStatus.Success;
    }
}