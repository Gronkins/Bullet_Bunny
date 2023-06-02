using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Checks if a hazard collides with the player's hurtbox
        if (collider.CompareTag("Hazards") && gameObject.CompareTag("PlayerHurtbox"))
        {
            Debug.Log("Player collided with hazard");
            playerStats.playerHealth -= 1;
        }
    }
}