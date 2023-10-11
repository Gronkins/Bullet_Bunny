using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    CollectibleManager collectibleManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        collectibleManager = FindObjectOfType<CollectibleManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Is touching collectible");
            playerMovement.numOfDashes = playerMovement.maxDashes; //Reset dashes
            collectibleManager.DeactivateCollectible(this); //Turn off the collectible
        }
    }
}
