using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected IdleStateData stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected float idleTime;
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,IdleStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.SetVelocity(0);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            Entity.SpriteFlip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    protected void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
