using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Is touching");
            playerStats.playerHealth -= 1;
        }

        if (collision.collider.tag == "PlayerWeapon")
        {
            //Debug.Log("Is touching");
            //Destroy(gameObject);
        }
    }
}
