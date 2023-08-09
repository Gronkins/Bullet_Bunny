using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubbleEnemy : Enemy
{
    private PlayerStats newPlayerStats;
    private PlayerMovement newPlayerMovement;
    [SerializeField] private float upwardsForce;

    protected override void Start() 
    {
        newPlayerStats = FindObjectOfType<PlayerStats>();
        newPlayerMovement = FindObjectOfType<PlayerMovement>();
    }
    
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");
        if (collider.tag == "PlayerWeapon" && newPlayerStats.playerHealth > 0)
        {
            Debug.Log("Player hit enemy with weapon");
            EnemyDeath();
        }

        if (collider.tag == "PlayerDownwardsWeapon" && newPlayerStats.playerHealth > 0)
        {
            Debug.Log("Player hit enemy with downwards attack");
            newPlayerMovement.ApplyUpwardsForce(upwardsForce);
            EnemyDeath();
        }
    }
}
