using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTransition : MonoBehaviour
{
    ScreenManager screenManager;
    TilemapRenderer tilemapRenderer;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        screenManager = FindObjectOfType<ScreenManager>();
        HideSprite();
    }

    private void HideSprite()
    {
        if (tilemapRenderer != null)
        {
            tilemapRenderer.material.color = new Color(1f, 1f, 1f, 0f); //Hides the level transition tile in-game
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0f); //Hides the level transition tile in-game
        }
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Is touching Transition");
            screenManager.LoadNextScene();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            Debug.Log("Is touching Transition");
            screenManager.LoadNextScene();
        }
    }
}
