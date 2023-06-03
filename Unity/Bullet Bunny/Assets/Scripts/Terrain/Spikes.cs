using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{
    PlayerStats playerStats;
    TilemapRenderer tilemapRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapRenderer.material.color = new Color(1f, 1f, 1f, 0f); //Hides the red hazards in-game
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Used for testing
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Is touching");
        }

        if (collision.collider.tag == "PlayerWeapon")
        {

        }
    }
}
