using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        //Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(armPos.position, attackRange, enemyLayers);

        //foreach (Collider2D enemy in hitEnemy)
        //{
        //    if (enemy.gameObject.CompareTag("MeleeEnemy"))
        //    {
        //        enemy.GetComponent<CharacterStats>().TakeDamage(myStats.damage.GetValue());
        //    }
        //    if (enemy.gameObject.CompareTag("RangedEnemy"))
        //    {
        //        enemy.GetComponent<RangedEnemyBehavior>().TakeDamage(attackDamage);
        //    }
        //    if (enemy.gameObject.CompareTag("GhostEnemy"))
        //    {
        //        enemy.GetComponent<GhostEnemyBehavior>().TakeDamage(attackDamage);
        //    }
        //}
        //targetStats.TakeDamage(myStats.damage.GetValue());
    }
    
}
