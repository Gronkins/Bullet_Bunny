using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTransition : MonoBehaviour
{
    ScreenManager screenManager;
    TilemapRenderer tilemapRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //tilemapRenderer = FindObjectOfType<TilemapRenderer>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        screenManager = FindObjectOfType<ScreenManager>();
        tilemapRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Is touching Transition");
            screenManager.LoadNextScene();
        }
    }
}
