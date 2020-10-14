using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Death))]
public class FigureDeath : MonoBehaviour
{
    [SerializeField] GameObject corpse;
    Death death;

    Holder holder;

    void Awake()
    {
        death = GetComponent<Death>();
        death.OnDeath += OnDeath;

        holder = GetComponent<Holder>();
    }

    void OnDeath(GameObject killer)
    {
        Instantiate(corpse, transform.position, transform.rotation);

        holder.Drop();

        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = new Vector3(-100, 0, -100);
    }
}
