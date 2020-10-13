using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] LayerMask interactLayers;
    [SerializeField] float interactDistance = 3;

    [Header("Collect")]
    Holder holder;

    [Header("Agent")]
    [SerializeField] LayerMask agentLayers;
    [SerializeField] float agentDistance = 10;

    Camera _camera;
    Interactable interacting;

    public delegate void InteractCallback(Interactable interactable);
    public event InteractCallback OnInteracted;

    public delegate void DisinteractCallback();
    public event DisinteractCallback OnDisinteracted;

    void Awake()
    {
        holder = GetComponent<Holder>();
        _camera = Camera.main;
    }

    void Update()
    {
        RayTest();
    }

    void RayTest()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayers))
        {
            var _object = hit.collider.gameObject;
            SetInteracting(_object);
            if (!interacting) return;

            if (Input.GetButtonDown("Pick"))
            {
                holder.Collect(interacting);
                // OnCollected?.Invoke(interacting);
            }
        }
        else if (Physics.Raycast(ray, out hit, agentDistance, agentLayers))
        {
            var _object = hit.collider.gameObject;
            SetInteracting(_object);
        }
        else SetDisinteracting();
    }

    void SetInteracting(GameObject _object)
    {
        if (interacting && interacting.gameObject == _object) return;
        Debug.Log($"Player interacting {_object.ToString()}");

        var next = _object.GetComponent<Interactable>();
        OnInteracted?.Invoke(next);
        interacting = next;
    }

    void SetDisinteracting()
    {
        if (interacting != null) OnDisinteracted?.Invoke();
        interacting = null;
    }
}
