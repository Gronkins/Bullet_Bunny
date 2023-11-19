using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTransition : MonoBehaviour
{
    public int currentStage;
    public bool isEndOfStage;
    ScreenManager screenManager;

    private void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (isEndOfStage)
            {
                EndOfStageTransition();
            }
            else
            {
                screenManager.LoadNextScene();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (isEndOfStage)
            {
                EndOfStageTransition();
            }
            else
            {
                screenManager.LoadNextScene();
            }
        }
    }

    private void EndOfStageTransition()
    {
        GameManager.Instance.ClearPauseMenu();
        GameManager.Instance.currentStage = currentStage;
        screenManager.LoadEndOfLevelScreen();
    }
}
