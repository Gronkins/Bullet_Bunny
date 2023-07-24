using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{
    PlayerStats playerStats;
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer tilemapRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetMaterial();
    }

    private void SetMaterial()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0f); //Hides the red hazards in-game
        }

        if (tilemapRenderer != null)
        {
            tilemapRenderer.material.color = new Color(1f, 1f, 1f, 0f); //Hides the red hazards in-game
        }
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
