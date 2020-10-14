using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] List<Collectable> collectables;
    Dictionary<Collectable, Vector3> collectablePositions = new Dictionary<Collectable, Vector3>();

    void Awake()
    {
        foreach (var collectable in collectables)
            collectablePositions.Add(collectable, collectable.transform.position);
    }

    public bool GetCollectablePosition(Collectable collectable, out Vector3 position)
    {
        if (collectablePositions.ContainsKey(collectable))
        {
            position = collectablePositions[collectable];
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    public void SetCollectablePosition(Collectable collectable, Vector3 position) => collectablePositions[collectable] = position;
}
