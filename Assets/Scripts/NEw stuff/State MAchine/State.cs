using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine StateMachine;
    protected Entity Entity;

    protected float startTime;
    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.Entity = entity;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        Entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        Entity.anim.SetBool(animBoolName, false);

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

}
