using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected MoveStateData stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,MoveStateData stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = Entity.EdgeCheck();
        isDetectingWall = Entity.WallCheck();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingLedge = Entity.EdgeCheck();
        isDetectingWall = Entity.WallCheck();
    }
}
