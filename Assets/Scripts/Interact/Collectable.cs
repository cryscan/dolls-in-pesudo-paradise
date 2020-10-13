using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Collectable : MonoBehaviour
{
    [SerializeField] float positionFallout = 20;
    [SerializeField] float rotationFallout = 10;

    Interactable interactable;
    Holder holder;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteracted += OnCollected;
        interactable.OnInteracted += OnDropped;
        interactable.RegisterAction(ActionType.Collect);
        interactable.RegisterAction(ActionType.Drop);
    }

    void Update()
    {
        if (!holder) return;

        var position = transform.position;
        var rotation = transform.rotation;
        var hold = holder.hold;

        position = Vector3.Lerp(position, hold.position, 1 - Mathf.Exp(-positionFallout * Time.deltaTime));
        rotation = Quaternion.Slerp(rotation, hold.rotation, 1 - Mathf.Exp(-rotationFallout * Time.deltaTime));

        transform.position = position;
        transform.rotation = rotation;
    }

    void OnCollected(GameObject subject, ActionType action)
    {
        if (action != ActionType.Collect) return;

        holder = subject.GetComponent<Holder>();
        if (!holder) return;

        // holder.SetHolding(this);

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        var collider = GetComponent<Collider>();
        if (collider) collider.enabled = false;
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

        var collider = GetComponent<Collider>();
        if (collider) collider.enabled = true;
    }
}
