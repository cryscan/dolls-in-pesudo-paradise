using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Transform hold;
    [SerializeField] float collectDistance = 1;
    [SerializeField] LayerMask collectLayers;

    [SerializeField] float positionFallout = 10;
    [SerializeField] float rotationFallout = 10;

    Camera _camera;
    GameObject holding;

    public delegate void CollectCallback(GameObject _object);
    public event CollectCallback OnCollected;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (holding)
        {
            var position = holding.transform.position;
            var rotation = holding.transform.rotation;

            position = Vector3.Lerp(position, hold.position, 1 - Mathf.Exp(-positionFallout * Time.deltaTime));
            rotation = Quaternion.Slerp(rotation, hold.rotation, 1 - Mathf.Exp(-rotationFallout * Time.deltaTime));

            holding.transform.position = position;
            holding.transform.rotation = rotation;
        }
    }

    void FixedUpdate()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, collectDistance, collectLayers))
        {
            var _object = hit.collider.gameObject;
            if (!_object.CompareLayer(collectLayers)) return;

            if (Input.GetButtonDown("Fire1"))
            {
                ExchangeHolding(_object);
                OnCollected?.Invoke(_object);
            }
        }
    }

    void ExchangeHolding(GameObject _object)
    {
        Rigidbody rb;
        Collider collider;

        if (holding)
        {
            rb = holding.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false;

            collider = holding.GetComponent<Collider>();
            if (collider) collider.enabled = true;
        }

        holding = _object;

        rb = holding.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        collider = holding.GetComponent<Collider>();
        if (collider) collider.enabled = false;
    }
}
