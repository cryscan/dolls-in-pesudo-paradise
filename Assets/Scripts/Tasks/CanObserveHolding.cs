using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanObserveHolding : Conditional
{
    public Observer observer;
    public Holder holder;
    public Interactable interactable;

    public override TaskStatus OnUpdate()
    {
        if (observer.GetActions(interactable).Contains(ActionType.Observe) && holder.holding && (holder.holding.gameObject == interactable.gameObject))
            return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}