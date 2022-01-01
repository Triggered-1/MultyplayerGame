using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat MaxHealth;
    public float CurrentHealth;
    public Stat damage;
    public Stat Armor;

    public Stat CritChance;
    public Stat CritDamageMultiplier;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth.GetValue();
    }

    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some Way
        //THis method is meant to be overwritten

        Debug.Log(transform.name + " Died");
    }
}
