using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyBehavior : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField] private Transform target;
    private Transform[] enemyTransforms;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Vector2 bottomOffset;
    
    private bool isFacingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        enemyTransforms = GetPlayerTranforms();
        target = GetClosestEnemy(enemyTransforms);
    }

    private void FixedUpdate()
    {
        target = GetClosestEnemy(enemyTransforms);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        if (transform.position.x < target.position.x && !isFacingRight)
        {
            SpriteFlip();
        }
        else if (transform.position.x > target.position.x && isFacingRight)
        {
            SpriteFlip();
        }
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

    private void SpriteFlip()
    {
        transform.Rotate(bottomOffset,180f,0f);
        isFacingRight = !isFacingRight;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
