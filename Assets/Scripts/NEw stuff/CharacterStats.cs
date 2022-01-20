using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterStats : MonoBehaviourPun
{
    public Stat MaxHealth;
    public float CurrentHealth;
    public Stat damage;
    public Stat Armor;

    public Stat CritChance;
    public Stat CritDamageMultiplier;
    public Transform DamagePopUpPrefab;

    public System.Action onDeath;

    //public Rigidbody2D rb { get; private set; }
    //public Animator anim { get; private set; }
    //public GameObject aliveGO { get; private set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth.GetValue();
    }

    public virtual void Start()
    {
        //aliveGO = transform.Find("Alive").gameObject;
        //rb = aliveGO.GetComponent<Rigidbody2D>();
        //anim = aliveGO.GetComponent<Animator>();
    }

    [PunRPC]
    private void TakeDamageRPC(float damage, bool isCrit)
    {
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        CurrentHealth -= damage;
        DamagePopUp.Create(transform.position, damage, isCrit, DamagePopUpPrefab);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage,bool isCrit)
    {
        photonView.RPC(nameof(TakeDamageRPC), RpcTarget.AllBuffered,damage,isCrit);
    }

    public virtual void Die()
    {
        //Die in some Way
        //THis method is meant to be overwritten
        onDeath?.Invoke();
        Debug.Log(transform.name + " Died");
        photonView.RPC("DestroyEnemyRPC", RpcTarget.MasterClient);
    }


    [PunRPC]
    private void DestroyEnemyRPC()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
