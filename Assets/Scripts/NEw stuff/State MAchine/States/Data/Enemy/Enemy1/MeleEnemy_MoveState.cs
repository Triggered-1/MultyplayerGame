using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy_MoveState : MoveState
{
    private MeleEnemy enemy;

    public MeleEnemy_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData,MeleEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
