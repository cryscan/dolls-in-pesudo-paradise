using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AimAt : Action
{
    public SharedGameObject subject;
    public SharedGameObject target;
    public float tolerance = 1;
    public float fallout = 10;

    public override TaskStatus OnUpdate()
    {
        var trans = subject.Value.transform;

        var direction = target.Value.transform.position - trans.position;
        direction.y = 0;
        var angle = Vector3.SignedAngle(trans.forward, direction, Vector3.up);

        trans.Rotate(0, angle * (1 - Mathf.Exp(-fallout * Time.deltaTime)), 0);
        if (angle < tolerance) return TaskStatus.Success;
        else return TaskStatus.Running;
    }
}