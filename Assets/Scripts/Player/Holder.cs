using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Holder : MonoBehaviour, Interactor
{
    [SerializeField] Transform _hold;
    public Transform hold { get => _hold; }

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
        List<ActionType> actions = new List<ActionType>();
        if (interactable.actions.Contains(ActionType.Collect)) actions.Add(ActionType.Collect);
        return actions;
    }
}
