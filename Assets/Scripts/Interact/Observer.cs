using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour, Interactor
{
    [Header("Detect")]
    [SerializeField] float detectRange = 4;

    public List<ActionType> GetActions(Interactable interactable)
    {
        var actions = new List<ActionType>();

        var position = interactable.gameObject.transform.position;
        var direction = position - transform.position;

        if (direction.magnitude > detectRange) return actions;

        if (interactable.actions.Contains(ActionType.Observe)) actions.Add(ActionType.Observe);
        return actions;
    }
}
