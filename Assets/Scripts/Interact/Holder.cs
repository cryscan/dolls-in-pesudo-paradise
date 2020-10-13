using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Holder : MonoBehaviour, Interactor
{
    [SerializeField] Transform _hold;
    public Transform hold { get => _hold; }

    [SerializeField] float detectRange = 4;
    [SerializeField] LayerMask blockLayers;

    Collectable _holding;
    public Collectable holding { get => _holding; }

    public void Drop()
    {
        if (_holding)
        {
            // Drop previous holding.
            var previous = _holding.gameObject.GetComponent<Interactable>();
            previous?.Interact(gameObject, ActionType.Drop);
        }
        _holding = null;
    }

    public void Collect(Interactable interactable)
    {
        if (_holding)
        {
            // Drop previous holding.
            var previous = _holding.gameObject.GetComponent<Interactable>();
            previous?.Interact(gameObject, ActionType.Drop);
        }

        // Collect the object.
        interactable?.Interact(gameObject, ActionType.Collect);
        _holding = interactable.gameObject.GetComponent<Collectable>();
    }

    public List<ActionType> GetActions(Interactable interactable)
    {
        var actions = new List<ActionType>();

        if (holding == null || interactable.gameObject != holding.gameObject)
        {
            if (interactable.actions.Contains(ActionType.Collect))
                actions.Add(ActionType.Collect);
        }
        else actions.Add(ActionType.Drop);
        return actions;
    }

    public List<ActionType> DetectActions(Interactable interactable)
    {
        var position = interactable.gameObject.transform.position;
        var direction = position - transform.position;
        direction.y = 0;

        if (direction.magnitude > detectRange) return null;
        if (Physics.Raycast(transform.position, direction, detectRange, blockLayers)) return null;

        return GetActions(interactable);
    }
}
