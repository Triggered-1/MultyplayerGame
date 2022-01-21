using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Animator attackAnim;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    private bool canAttack = true;
    
    [SerializeField] private Transform playerPos;
    private Transform[] enemyTransforms;

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

    [SerializeField] private float wallCheckRadius;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheckPos;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    
    private bool isFaceingRight = true;
    private bool canMove = true;
    //private bool canShoot = true;
    private bool isGrounded;
    private bool wallCheck;
    private bool edgeCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyTransforms = GetPlayerTranforms();
        playerPos = GetClosestEnemy(enemyTransforms);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);

        playerPos = GetClosestEnemy(enemyTransforms);
        Move();
        Vector2 toPlayer = playerPos.position - transform.position;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, toPlayer, rayDistance,obstacleLayer);
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
            if (canAttack)
            {
                toPlayer.x = Mathf.Abs(toPlayer.x);
                float angle = Vector2.Angle(Vector2.right, toPlayer);

                if (angle < 30f)
                {
                    StartCoroutine(AttackSequence());
                }
            }
        }
        else if (!rayHit)
        {
            canMove = true;
        }
            
        Debug.DrawRay(transform.position, playerPos.position - transform.position);
    }

    private Transform[] GetPlayerTranforms()
    {
        PlayerController[] playerControllerArray = FindObjectsOfType<PlayerController>();
        Transform[] playerPositons = new Transform[playerControllerArray.Length];

        for (int i = 0; i < playerControllerArray.Length; i++)
        {
            playerPositons[i] = playerControllerArray[i].transform;
        }
        return playerPositons;
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    private void Move()
    {
        if (EdgeCheck() && canMove)
        {
            SpriteFlip();
        }
        if (WallCheck() && canMove)
        {
            SpriteFlip();
        }
        
        if (isGrounded && canMove)
        {
            anim.SetBool("Run", true);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run",false);
        }
    }

    private bool EdgeCheck()
    {
        bool result = !Physics2D.OverlapCircle(edgeCheckPos.position, edgeCheckRadius, edgeLayer);

        return result;
    }
    
    private bool WallCheck()
    {
        bool result = Physics2D.OverlapCircle(wallCheckPos.position, wallCheckRadius, wallLayer);

        return result;
    }

    private void SpriteFlip()
    {
        transform.Rotate(bottomOffset,180f,0f);
        isFaceingRight = !isFaceingRight;
    }

    private void Attack()
    {
        attackAnim.SetTrigger("Attack");
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(shootPos.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("we hit " + enemy.name);
            if (enemy.gameObject.CompareTag("Player"))
            {
                enemy.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator AttackSequence()
    {
        canAttack = false;
        Attack();
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] {bottomOffset, rightOffset, rightbottomOffset};
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);
        Gizmos.DrawWireSphere(edgeCheckPos.position, edgeCheckRadius);
        Gizmos.DrawWireSphere(wallCheckPos.position, wallCheckRadius);
        Gizmos.DrawWireSphere(shootPos.position, attackRange);
    }
}