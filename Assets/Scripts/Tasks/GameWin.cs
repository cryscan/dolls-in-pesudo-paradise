using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GameWin : Action
{
    public int index;

    public override TaskStatus OnUpdate()
    {
        GameManager.instance.Win(index);
        return TaskStatus.Success;
    }
}