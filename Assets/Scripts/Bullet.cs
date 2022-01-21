using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int bulletDamage;

    Rigidbody2D rb;
    Vector2 moveDirection;

    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player"); ;
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
