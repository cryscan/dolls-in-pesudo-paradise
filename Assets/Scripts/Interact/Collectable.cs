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
    List<DropPoint> dropPoints;

    Holder _holder;
    public Holder holder { get => _holder; }

    Transform target;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteracted += OnCollected;
        interactable.OnInteracted += OnDropped;
        interactable.RegisterAction(ActionType.Collect);
        interactable.RegisterAction(ActionType.Drop);
        interactable.RegisterAction(ActionType.Observe);

        dropPoints = FindObjectsOfType<DropPoint>().ToList();
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

    void OnCollected(GameObject subject, ActionType action, Interactable.Data data)
    {
        if (action != ActionType.Collect) return;

        _holder = subject.GetComponent<Holder>();
        if (!_holder) return;

        // holder.SetHolding(this);

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        SetEnabledColliders(false);

        target = _holder.hold;
    }

    void OnDropped(GameObject subject, ActionType action, Interactable.Data data)
    {
        if (action != ActionType.Drop) return;

        Debug.Assert(subject.GetComponent<Holder>() == _holder);
        Debug.Assert(_holder.holding == this);

        // holder.SetHolding();
        _holder = null;

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;

        SetEnabledColliders(true);

        var candidates = dropPoints.FindAll(x => Vector3.Distance(x.transform.position, transform.position) < dropRange);
        if (candidates.Count > 0)
        {
            var min = candidates.Min(x => x.priority);
            target = candidates.Find(x => x.priority == min).transform;
        }
        else target = null;

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
