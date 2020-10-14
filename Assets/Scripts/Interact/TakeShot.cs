using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TakeShot : MonoBehaviour
{
    public class ShootData : Interactable.Data
    {
        public Vector3 position;
        public Vector3 force;
        public bool center;
    }

    Interactable interactable;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteracted += OnTakenShot;
        interactable.RegisterAction(ActionType.Shoot);
    }

    void OnTakenShot(GameObject subject, ActionType action, Interactable.Data data)
    {
        if (action != ActionType.Shoot) return;

        var rb = GetComponent<Rigidbody>();
        ShootData shootData = (ShootData)data;
        if (rb && (shootData != null))
        {
            Debug.Log($"{subject.name} shoots {gameObject.name}");
            if (shootData.center) rb.AddForce(shootData.force);
            else rb.AddForceAtPosition(shootData.force, shootData.position);
        }

        var death = GetComponent<Death>();
        if (death)
        {
            Debug.Log($"{subject.name} kills {gameObject.name}");
            death.Kill(subject);
        }
    }
}
