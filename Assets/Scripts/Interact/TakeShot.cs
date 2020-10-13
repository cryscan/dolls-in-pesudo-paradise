using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TakeShot : MonoBehaviour
{
    [SerializeField] float force = 500;
    Interactable interactable;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteracted += OnTakenShot;
        interactable.RegisterAction(ActionType.Shoot);
    }

    void OnTakenShot(GameObject subject, ActionType action)
    {
        if (action != ActionType.Shoot) return;

        var rb = GetComponent<Rigidbody>();
        if (rb)
        {
            Debug.Log($"{subject.name} shoots {gameObject.name}");
            var direction = transform.position - subject.transform.position;
            direction.y = 0;
            rb.AddForce(force * direction.normalized);
        }

        var death = GetComponent<Death>();
        if (death)
        {
            Debug.Log($"{subject.name} kills {gameObject.name}");
            death.Kill(subject);
        }
    }
}
