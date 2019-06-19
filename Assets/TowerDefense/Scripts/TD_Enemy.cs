using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TD_Enemy : MonoBehaviour
{

    public int healthMax;
    int healthCurrent;
    private NavMeshAgent na;
    // Use this for initialization
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        healthCurrent -= damage;
        if (healthCurrent <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
