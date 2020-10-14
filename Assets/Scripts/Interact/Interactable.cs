using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public abstract class Data { };

    public List<ActionType> actions { get; private set; } = new List<ActionType>();

    public delegate void InteractCallback(GameObject subject, ActionType action, Data data = null);
    public event InteractCallback OnInteracted;

    public void Interact(GameObject subject, ActionType action, Data data = null)
    {
        OnInteracted?.Invoke(subject, action, data);
    }

    public void RegisterAction(ActionType action) => actions.Add(action);
}
