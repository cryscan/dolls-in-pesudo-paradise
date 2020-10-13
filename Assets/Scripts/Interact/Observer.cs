using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour, Interactor
{
    [Header("Detect")]
    [SerializeField] Transform eye;
    [SerializeField] float detectRange = 4;
    [SerializeField] LayerMask blockLayers;

    public List<ActionType> GetActions(Interactable interactable)
    {
        var actions = new List<ActionType>();
        if (interactable.actions.Contains(ActionType.Observe)) actions.Add(ActionType.Observe);
        return actions;
    }

    public List<ActionType> DetectActions(Interactable interactable)
    {
        var position = interactable.gameObject.transform.position;
        var direction = position - eye.transform.position;

        if (direction.magnitude > detectRange) return null;
        if (Physics.Raycast(transform.position, direction, detectRange, blockLayers)) return null;

        return GetActions(interactable);
    }
}
