using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Holder))]
public class Shoot : MonoBehaviour, Interactor
{
    [SerializeField] float positionOffset = 0.2f;
    [SerializeField] float rotationOffset = 20;

    [SerializeField] float cooldown = 1;
    Holder holder;

    bool _canShoot = true;
    public bool canShoot { get => _canShoot; }

    void Awake()
    {
        holder = GetComponent<Holder>();
    }

    public void Fire(Interactable interactable)
    {
        if (!_canShoot || !HoldingGun()) return;

        var _transform = holder.holding.gameObject.transform;
        _transform.position += -positionOffset * holder.hold.forward;
        _transform.Rotate(-rotationOffset, 0, 0, Space.Self);

        interactable.Interact(gameObject, ActionType.Shoot);
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    bool HoldingGun() => holder.holding && holder.holding.gameObject.CompareTag("Handgun");

    public List<ActionType> GetActions(Interactable interactable)
    {
        var actions = new List<ActionType>();
        if (canShoot && HoldingGun() && interactable.actions.Contains(ActionType.Shoot))
            actions.Add(ActionType.Shoot);
        return actions;
    }
}
