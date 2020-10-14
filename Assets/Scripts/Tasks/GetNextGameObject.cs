using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetNextGameObject : Action
{
    public SharedGameObjectList gameObjectList;
    public SharedGameObject storedGameObject;

    public override TaskStatus OnUpdate()
    {
        if (gameObjectList.Value.Count == 0) return TaskStatus.Failure;

        storedGameObject.Value = gameObjectList.Value.First();
        gameObjectList.Value.RemoveAt(0);

        return TaskStatus.Success;
    }
}