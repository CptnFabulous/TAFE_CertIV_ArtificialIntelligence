using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 1;
    public float attackRate = 1f;
    public float attackRange;
    float attackTimer;

    protected TD_Enemy currentEnemy;

    public virtual void Aim(TD_Enemy e) { }
    public virtual void Attack(TD_Enemy e) { }

    void DetectEnemies()
    {
        // Reset current enemy
        currentEnemy = null;
        // Perform OverlapSphere and get the hits
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through everything we hit
        foreach (var hit in hits)
        {
            // If the thing we hit is an enemy
            TD_Enemy enemy = hit.GetComponent<TD_Enemy>();
            if (enemy)
            {
                // Set current enemy to that one
                currentEnemy = enemy;
            }
        }
    }


    // Protected - Accessible to Cannon / Other Tower classes
    // Virtual - Able to change what this function does in derived classes
    protected virtual void Update()
    {
        // Detect enemies before performing attack logic
        DetectEnemies();
        // Count up the timer
        attackTimer += Time.deltaTime;
        // if there's an enemy
        if (currentEnemy)
        {
            // Aim at the enemy
            Aim(currentEnemy);
            // Is attack timer ready?
            if (attackTimer >= attackRate)
            {
                // Attack the enemy!
                Attack(currentEnemy);
                // Reset timer
                attackTimer = 0f;
            }
        }
    }
}
