using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : Entity
{
    public MeleEnemy_IdleState idleState { get; private set; }
    public MeleEnemy_MoveState moveState { get; private set; }
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new MeleEnemy_MoveState(this,stateMachine,"move",moveStateData,this);
    }
}
