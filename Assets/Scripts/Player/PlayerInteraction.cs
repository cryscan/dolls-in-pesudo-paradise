using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] LayerMask promptLayers;
    [SerializeField] float promptDistance = 3;
    Interactable prompting;

    [Header("Collect")]
    [SerializeField] LayerMask collectLayers;
    [SerializeField] float collectDistance = 3;
    Holder holder;

    Shoot shoot;

    Camera _camera;

    public delegate void InteractCallback(Interactable interactable);
    public event InteractCallback OnInteracted;

    public delegate void DisinteractCallback();
    public event DisinteractCallback OnDisinteracted;

    void Awake()
    {
        holder = GetComponent<Holder>();
        shoot = GetComponent<Shoot>();
        _camera = Camera.main;
    }

    void Update()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, promptDistance, promptLayers))
        {
            var _object = hit.collider.gameObject;
            SetInteracting(_object);
        }
        else SetDisinteracting();

        if (Input.GetButtonDown("Pick"))
        {
            if (Physics.Raycast(ray, out hit, collectDistance, collectLayers))
            {
                var interactable = hit.collider.gameObject.GetComponent<Interactable>();
                if (interactable) holder.Collect(interactable);
            }
            else holder.Drop();
        }

        if (Input.GetButtonDown("Fire1") && Physics.Raycast(ray, out hit))
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            shoot.Fire(interactable);
        }
    }

    void SetInteracting(GameObject _object)
    {
        if (prompting && prompting.gameObject == _object) return;
        Debug.Log($"Player interacting {_object.name}");

        var next = _object.GetComponent<Interactable>();
        OnInteracted?.Invoke(next);
        prompting = next;
    }

    void SetDisinteracting()
    {
        if (prompting != null) OnDisinteracted?.Invoke();
        prompting = null;
    }
}
