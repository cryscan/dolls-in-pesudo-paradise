using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ControlPromptLister))]
public class ControlHints : MonoBehaviour
{
    ControlPromptLister lister;
    Camera _camera;

    GameObject player;
    PlayerInteraction interaction;
    Collider interactingCollider;

    void Awake()
    {
        _camera = Camera.main;

        lister = GetComponent<ControlPromptLister>();

        player = GameObject.FindGameObjectWithTag("Player");
        interaction = player?.GetComponent<PlayerInteraction>();
        interaction.OnInteracted += OnInteracted;
        interaction.OnDisinteracted += () => lister.SetActiveAll(false);
    }

    void Start()
    {
        lister.SetActiveAll(false);
    }

    void Update()
    {
        Relocate();
    }

    void Relocate()
    {
        if (interactingCollider)
        {
            var bounds = interactingCollider.bounds;
            transform.position = _camera.WorldToScreenPoint(bounds.center);
        }
    }

    void OnInteracted(Interactable interactable)
    {
        if (!interactable) return;
        lister.SetActiveAll(false);

        var actions = new List<ActionType>();
        var interactors = player.GetComponents<Interactor>();
        foreach (var interactor in interactors)
            actions = actions.Union(interactor.GetActions(interactable)).ToList();

        foreach (var action in actions)
            lister.Find(action)?.SetActive(true);

        interactingCollider = interactable.gameObject.GetComponent<Collider>();
        Relocate();
    }

    void OnDisinteracted()
    {
        lister.SetActiveAll(false);
        interactingCollider = null;
    }
}
