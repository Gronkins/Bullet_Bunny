using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    protected Animator animator;
    public GameObject explosionPrefab;
    protected bool isAlive;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");
        if (collider.tag == "PlayerWeapon" && playerStats.playerHealth > 0)
        {
            Debug.Log("Player hit enemy with weapon");
            EnemyDeath();
        }

        if (collider.tag == "PlayerDownwardsWeapon" && playerStats.playerHealth > 0)
        {
            Debug.Log("Player hit enemy with downwards attack");
            playerMovement.ApplyUpwardsForce();
            EnemyDeath();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && isAlive == true)
        {
            Debug.Log("Enemy hit player");
            DealDamageToPlayer();
        }

        
        if (collision.collider.tag == "PlayerWeapon")
        {
            Debug.Log("Is hit with player Weapon");
            EnemyDeath();
        }
    }

    protected virtual void DealDamageToPlayer()
    {
        playerStats.playerHealth -=1;
    }

    protected virtual void EnemyDeath()
    {
        isAlive = false;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
