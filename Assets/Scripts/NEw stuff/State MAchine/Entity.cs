using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public FiniteStateMachine stateMachine;
    public CharacterStats entityStats;
    public EntityData entityData;
    public int FacingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }

    private Vector2 velocityWorkspace;
    [SerializeField] private Transform wallCheckPos;
    [SerializeField] private Transform edgeCheckPos;
    // Start is called before the first frame update
    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(FacingDirection * velocity, rb.velocity.y);
    }

    public virtual bool EdgeCheck()
    {
        bool result = !Physics2D.OverlapCircle(edgeCheckPos.position, entityData.edgeCheckDistance, entityData.groundLayer);

        return result;
    }

    public virtual bool WallCheck()
    {
        bool result = Physics2D.OverlapCircle(wallCheckPos.position, entityData.wallCheckDistance, entityData.groundLayer);

        return result;
    }

    public virtual void SpriteFlip()
    {
        //transform.Rotate(bottomOffset, 180f, 0f);
        //isFaceingRight = !isFaceingRight;

        FacingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f); 
    }
}
