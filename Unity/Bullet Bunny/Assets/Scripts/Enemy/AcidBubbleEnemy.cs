using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubbleEnemy : Enemy
{
    private PlayerStats newPlayerStats;
    private PlayerMovement newPlayerMovement;
    [SerializeField] private float upwardsForce = 25f;

    protected override void Start() 
    {
        newPlayerStats = FindObjectOfType<PlayerStats>();
        newPlayerMovement = FindObjectOfType<PlayerMovement>();
        isAlive = true;
    }
    
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");
        if (collider.tag == "PlayerWeapon")
        {
            Debug.Log("Player hit enemy with weapon");
            EnemyDeath();
        }

        if (collider.tag == "PlayerDownwardsWeapon")
        {
            Debug.Log("Player hit enemy with downwards attack");
            newPlayerMovement.ApplyUpwardsForce(upwardsForce);
            EnemyDeath();
        }
    }

    protected override void DealDamageToPlayer()
    {
        newPlayerStats.playerHealth -=1;
    }
}
