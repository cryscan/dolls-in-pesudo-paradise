using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<ActionType> actions { get; private set; } = new List<ActionType>();

    public delegate void InteractCallback(GameObject subject, ActionType action);
    public event InteractCallback OnInteracted;

    public void Interact(GameObject subject, ActionType action)
    {
        OnInteracted?.Invoke(subject, action);
    }

    public void RegisterAction(ActionType action) => actions.Add(action);
}
