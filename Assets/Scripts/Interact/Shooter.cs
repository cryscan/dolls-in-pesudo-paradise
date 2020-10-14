using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Holder))]
public class Shooter : MonoBehaviour, Interactor
{
    [SerializeField] float positionOffset = 0.2f;
    [SerializeField] float rotationOffset = 20;

    [SerializeField] float cooldown = 1;
    [SerializeField] float force = 500;

    [Header("Detect")]
    [SerializeField] LayerMask blockLayers;

    Holder holder;

    bool _canShoot = true;
    public bool canShoot { get => _canShoot; }

    public delegate void FireCallback();
    public event FireCallback OnFire;

    void Awake()
    {
        holder = GetComponent<Holder>();
    }

    public void Fire(Interactable interactable, TakeShot.ShootData shootData)
    {
        if (!_canShoot || !HoldingGun()) return;

        var trans = holder.holding.gameObject.transform;
        trans.position += -positionOffset * holder.hold.forward;
        trans.Rotate(-rotationOffset, 0, 0, Space.Self);

        OnFire?.Invoke();

        StartCoroutine(FireCoroutine());

        if (!interactable) return;

        if (shootData == null)
        {
            shootData = new TakeShot.ShootData();
            shootData.center = true;
        }

        var target = interactable.gameObject.transform;
        shootData.force = force * (target.position - holder.hold.position).normalized;

        interactable.Interact(gameObject, ActionType.Shoot, shootData);
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

        var position = interactable.gameObject.transform.position;
        var direction = position - holder.hold.transform.position;
        if (Physics.Raycast(transform.position, direction, direction.magnitude, blockLayers)) return actions;

        if (!canShoot || !HoldingGun()) return actions;
        if (interactable.actions.Contains(ActionType.Shoot)) actions.Add(ActionType.Shoot);

        return actions;
    }
}