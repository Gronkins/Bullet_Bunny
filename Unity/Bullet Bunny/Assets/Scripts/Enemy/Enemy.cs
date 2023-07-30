using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    protected Animator animator;
    public GameObject explosionPrefab;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Enemy hit player");
            playerStats.playerHealth -= 1;
        }

        
        if (collision.collider.tag == "PlayerWeapon")
        {
            Debug.Log("Is hit with player Weapon");
            EnemyDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
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

    private void EnemyDeath()
    {
        if (playerStats.playerHealth > 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
