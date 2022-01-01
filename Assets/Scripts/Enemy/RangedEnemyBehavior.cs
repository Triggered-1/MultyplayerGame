using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangedEnemyBehavior : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform shootPos;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float rayDistance;
    
    [SerializeField] private Vector2 bottomOffset, rightOffset, rightbottomOffset;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed;

    [SerializeField] private float edgeCheckRadius;
    [SerializeField] private LayerMask edgeLayer;
    [SerializeField] private Transform edgeCheckPos;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    //[SerializeField] private Bullet bullet;
    [SerializeField] private int bulletAmount;
    
    
    private bool isFaceingRight = true;
    private bool canMove = true;
    private bool canShoot = true;
    private bool isGrounded;
    private bool wallCheck;
    private bool edgeCheck;
    private static readonly int Run = Animator.StringToHash("Run");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);

        Move();
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, playerPos.position - transform.position,rayDistance,obstacleLayer);
        if (rayHit)
        {
            canMove = false;
            if (transform.position.x < playerPos.position.x && !isFaceingRight)
            {
                SpriteFlip();
            }
            else if (transform.position.x > playerPos.position.x && isFaceingRight)
            {
                SpriteFlip();
            }
            if (canShoot)
            {
                StartCoroutine(AttackSequence());
            }
            
        }
        else if (!rayHit)
        {
            canMove = true;
        }
            
        Debug.DrawRay(transform.position, playerPos.position - transform.position);
    }

    private void Move()
    {
        if (EdgeCheck() && canMove)
        {
            SpriteFlip();
        }
        
        if (isGrounded && canMove)
        {
            anim.SetBool(Run, true);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run",false);
        }
    }
    
    IEnumerator AttackSequence()
    {
        //Bullet newBullet = Instantiate(bullet,shootPos.position,transform.rotation);
        
        canShoot = false;
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("GetHit");
        if (currentHealth <= 0 )
        {
            anim.SetTrigger("Die");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private bool EdgeCheck()
    {
        bool result = !Physics2D.OverlapCircle(edgeCheckPos.position, edgeCheckRadius, edgeLayer);

        return result;
    }

    private void SpriteFlip()
    {
        transform.Rotate(bottomOffset,180f,0f);
        isFaceingRight = !isFaceingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] {bottomOffset, rightOffset, rightbottomOffset};
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);
        Gizmos.DrawWireSphere(edgeCheckPos.position, edgeCheckRadius);
    }
}
