using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControlPromptLister))]
public class ControlHints : MonoBehaviour
{
    ControlPromptLister lister;
    Camera _camera;

    PlayerInteraction interaction;
    Collider interactingCollider;

    void Awake()
    {
        _camera = Camera.main;

        lister = GetComponent<ControlPromptLister>();

        var player = GameObject.FindGameObjectWithTag("Player");
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

    void OnInteracted(GameObject _object)
    {
        lister.SetActiveAll(false);
        List<ActionType> actions;
        if (interaction.GetActionTypes(_object, out actions))
        {
            foreach (var action in actions)
                lister.Find(action)?.SetActive(true);

            interactingCollider = _object.GetComponent<Collider>();
            Relocate();
        }
    }

    void OnDisinteracted()
    {
        lister.SetActiveAll(false);
        interactingCollider = null;
    }
}
