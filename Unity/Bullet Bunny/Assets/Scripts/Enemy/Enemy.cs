using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");
        if (collider.tag == "PlayerWeapon")
        {
            Debug.Log("Player hit enemy with weapon");
            Destroy(gameObject);
        }

        if (collider.tag == "PlayerDownwardsWeapon")
        {
            Debug.Log("Player hit enemy with downwards attack");
            playerMovement.ApplyUpwardsForce();
            Destroy(gameObject);
        }
    }


}
