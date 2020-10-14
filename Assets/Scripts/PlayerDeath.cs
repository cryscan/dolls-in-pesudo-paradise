using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Death death;
    PlayerMovement movement;
    PlayerLook look;

    Holder holder;
    Shooter shooter;

    void Awake()
    {
        death = GetComponent<Death>();
        death.OnDeath += OnDeath;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        holder = GetComponent<Holder>();
        shooter = GetComponent<Shooter>();
    }

    void OnDeath(GameObject killer)
    {
        movement.enabled = false;
        look.enabled = false;

        holder.Drop();
        holder.enabled = false;
        shooter.enabled = false;

        Time.timeScale = 0.1f;
    }
}
