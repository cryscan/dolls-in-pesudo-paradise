using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetSeekPoints : Action
{
    public SharedGameObjectList storedGameObjectList;
    List<SeekPoint> seekPoints;

    public override void OnAwake()
    {
        seekPoints = Object.FindObjectsOfType<SeekPoint>().ToList();
        seekPoints.Sort((x, y) => x.priority - y.priority);
    }

    public override TaskStatus OnUpdate()
    {
        storedGameObjectList.Value = seekPoints.Select(x => x.gameObject).ToList();
        return TaskStatus.Success;
    }
}