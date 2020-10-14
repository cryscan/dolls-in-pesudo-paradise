using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetNearestCollectable : Action
{
    public SharedGameObject subject;
    public SharedString tag;
    public SharedGameObject storedGameObject;

    List<Collectable> collectables;

    public override void OnAwake()
    {
        collectables = Object.FindObjectsOfType<Collectable>().ToList().FindAll(x => x.gameObject.CompareTag(tag.Value));
    }

    public override TaskStatus OnUpdate()
    {
        float min = float.PositiveInfinity;
        Collectable result = null;
        foreach (var collectable in collectables)
        {
            if (collectable.holder != null) continue;
            var distance = Vector3.Distance(subject.Value.transform.position, collectable.transform.position);
            if (distance < min)
            {
                result = collectable;
                min = distance;
            }
        }

        storedGameObject.Value = result.gameObject;

        if (result) return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}