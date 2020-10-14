using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class PlayerShootEvent : MonoBehaviour
{
    Shooter shooter;
    BehaviorTree behaviorTree;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        shooter = player.GetComponent<Shooter>();
        behaviorTree = GetComponent<BehaviorTree>();
    }

    void Start()
    {
        shooter.OnFire += () => behaviorTree.SendEvent("Player Fired");
    }
}
