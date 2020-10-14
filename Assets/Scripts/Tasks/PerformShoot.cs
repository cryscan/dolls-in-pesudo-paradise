using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PerformShoot : Action
{
    public SharedGameObject subject;
    public SharedGameObject target;

    Shooter shooter;
    Interactable interactable;

    public override void OnStart()
    {
        shooter = subject.Value.GetComponent<Shooter>();
        interactable = target.Value.GetComponent<Interactable>();
    }

    public override TaskStatus OnUpdate()
    {
        TakeShot.ShootData shootData = new TakeShot.ShootData();
        shootData.center = true;
        shootData.position = Vector3.zero;
        shootData.force = Vector3.zero;

        shooter.Fire(interactable, shootData);

        return TaskStatus.Success;
    }
}