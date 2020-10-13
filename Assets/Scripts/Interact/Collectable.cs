using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Collectable : MonoBehaviour
{
    [SerializeField] float positionFallout = 20;
    [SerializeField] float rotationFallout = 10;
    [SerializeField] float dropRange = 2;

    Interactable interactable;
    Transform[] dropPoints;
    Holder holder;
    Transform target;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteracted += OnCollected;
        interactable.OnInteracted += OnDropped;
        interactable.RegisterAction(ActionType.Collect);
        interactable.RegisterAction(ActionType.Drop);
        interactable.RegisterAction(ActionType.Observe);

        dropPoints = GameObject.FindGameObjectsWithTag("Drop Point").Select(x => x.transform).ToArray();
    }

    void Update()
    {
        if (target)
        {
            var position = transform.position;
            var rotation = transform.rotation;

            position = Vector3.Lerp(position, target.position, 1 - Mathf.Exp(-positionFallout * Time.deltaTime));
            rotation = Quaternion.Slerp(rotation, target.rotation, 1 - Mathf.Exp(-rotationFallout * Time.deltaTime));

            transform.position = position;
            transform.rotation = rotation;
        }
    }

    void OnCollected(GameObject subject, ActionType action)
    {
        if (action != ActionType.Collect) return;

        holder = subject.GetComponent<Holder>();
        if (!holder) return;

        // holder.SetHolding(this);

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        SetEnabledColliders(false);

        target = holder.hold;
    }

    void OnDropped(GameObject subject, ActionType action)
    {
        if (action != ActionType.Drop) return;

        Debug.Assert(subject.GetComponent<Holder>() == holder);
        Debug.Assert(holder.holding == this);

        // holder.SetHolding();
        holder = null;

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;

        SetEnabledColliders(true);

        // Search for the nearest drop point.
        target = null;
        float minDistance = 1000;
        foreach (var point in dropPoints)
        {
            var distance = Vector3.Distance(point.position, transform.position);
            if (distance < dropRange && distance < minDistance)
            {
                target = point;
                minDistance = distance;
            }
        }
        StartCoroutine(DropCoroutine());
    }

    IEnumerator DropCoroutine()
    {
        while (target && Vector3.Distance(transform.position, target.position) > 0.1)
            yield return null;
        target = null;
    }

    void SetEnabledColliders(bool enabled)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders) collider.enabled = enabled;
    }
}
