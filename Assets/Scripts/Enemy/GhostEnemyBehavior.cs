using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyBehavior : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField] private Transform target;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");
        if (currentHealth <= 0 )
        {
            anim.SetTrigger("Die");
            Die();
        }
    } 
    
    private void Die()
    {
        Destroy(gameObject);
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
