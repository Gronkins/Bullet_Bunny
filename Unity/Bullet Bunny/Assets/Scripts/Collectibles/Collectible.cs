using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        //playerStats = GetComponent<PlayerStats>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Is touching collectible");
            playerMovement.numOfDashes = playerMovement.maxDashes;
            Destroy(gameObject);
        }
    }
}
