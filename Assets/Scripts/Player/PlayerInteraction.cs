using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Collect")]
    [SerializeField] Transform hold;
    [SerializeField] LayerMask collectLayers;
    [SerializeField] float collectDistance = 3;

    [SerializeField] float positionFallout = 10;
    [SerializeField] float rotationFallout = 10;

    [Header("Agent")]
    [SerializeField] LayerMask agentLayers;
    [SerializeField] float agentDistance = 10;

    Camera _camera;

    GameObject _interacting;
    public GameObject interacting { get => _interacting; }

    GameObject _holding;
    public GameObject holding { get => _holding; }

    public delegate void InteractCallback(GameObject _object);
    public event InteractCallback OnInteracted, OnCollected;

    public delegate void DisinteractCallback();
    public event DisinteractCallback OnDisinteracted;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, collectDistance, collectLayers))
        {
            var _object = hit.collider.gameObject;
            SetInteracting(_object);

            if (_object.CompareLayer(collectLayers) && Input.GetButtonDown("Pick"))
            {
                ExchangeHolding(_object);
                OnCollected?.Invoke(_object);
            }
        }
        else if (Physics.Raycast(ray, out hit, agentDistance, agentLayers))
        {
            var _object = hit.collider.gameObject;
            SetInteracting(_object);
        }
        else
        {
            if (_interacting != null) OnDisinteracted?.Invoke();
            _interacting = null;
        }

        if (_holding)
        {
            var position = _holding.transform.position;
            var rotation = _holding.transform.rotation;

            position = Vector3.Lerp(position, hold.position, 1 - Mathf.Exp(-positionFallout * Time.deltaTime));
            rotation = Quaternion.Slerp(rotation, hold.rotation, 1 - Mathf.Exp(-rotationFallout * Time.deltaTime));

            _holding.transform.position = position;
            _holding.transform.rotation = rotation;
        }
    }

    void SetInteracting(GameObject _object)
    {
        if (_interacting != _object)
        {
            Debug.Log($"Interacting {_object.ToString()}");
            OnInteracted?.Invoke(_object);
        }
        _interacting = _object;

    }

    void ExchangeHolding(GameObject _object)
    {
        Rigidbody rb;
        Collider collider;

        if (_holding)
        {
            rb = _holding.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false;

            collider = _holding.GetComponent<Collider>();
            if (collider) collider.enabled = true;
        }

        _holding = _object;

        rb = _holding.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        collider = _holding.GetComponent<Collider>();
        if (collider) collider.enabled = false;
    }

    public bool GetActionTypes(GameObject _object, out List<ActionType> actions)
    {
        actions = new List<ActionType>();

        if (_object.CompareLayer(collectLayers))
        {
            actions.Add(ActionType.Collect);
            return true;
        }

        return false;
    }
}
