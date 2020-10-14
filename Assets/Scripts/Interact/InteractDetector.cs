using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    Interactor[] interactors;
    Interactable[] interactables;

    void Awake()
    {
        interactors = GetComponents<Interactor>();
        interactables = FindObjectsOfType<Interactable>();
    }

    public List<ActionType> GetActions(Interactable interactable)
    {
        var results = new List<ActionType>();

        foreach (var interactor in interactors)
        {
            var actions = interactor.GetActions(interactable);
            if (actions == null) continue;
            results = results.Union(actions).ToList();
        }

        return results;
    }

    public Dictionary<Interactable, List<ActionType>> GetInteractableActions()
    {
        var results = new Dictionary<Interactable, List<ActionType>>();
        foreach (var interactor in interactors)
        {
            foreach (var interactable in interactables)
            {
                var actions = interactor.GetActions(interactable);
                if (actions == null) continue;

                if (!results.ContainsKey(interactable)) results.Add(interactable, actions);
                else
                {
                    var previous = results[interactable];
                    var next = previous.Union(actions).ToList();
                    results[interactable] = next;
                }
            }
        }
        return results;
    }
}
