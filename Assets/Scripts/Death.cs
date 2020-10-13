using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool dead { get; private set; } = false;

    public delegate void DeathCallback(GameObject killer);
    public event DeathCallback OnDeath;

    public void Kill(GameObject killer)
    {
        dead = true;
        OnDeath?.Invoke(killer);
    }
}
