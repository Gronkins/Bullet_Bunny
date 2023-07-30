using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    PlayerStats playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(512, 288, true);
        playerStats = FindObjectOfType<PlayerStats>();
        //playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cylcing through levels with 1, and 2 for testing
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadPreviousScene();
        }

        //Closes the game
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //If the player resets zero or less HP, calls the death state
        if (playerStats.playerHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    //Waits for a moment, then restarts the current scene
    IEnumerator PlayerDeath()
    {
        Destroy(playerStats.upHitbox);
        Destroy(playerStats.sideHitbox);
        yield return new WaitForSeconds(0.3f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return null;
    }

    void SetScreenSize()
    {
        bool fullscreen = Screen.fullScreen;
        int width = 512;
        int height = 288;
        Screen.SetResolution(width, height, fullscreen);
    }

    public void LoadNextScene()
    {
        //Loads the next scene, if at the final scene, goes back to the first one
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex += 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void LoadPreviousScene()
    {
        //Loads the previous scene, if at the first scene, loads the final scene
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int previousSceneIndex = currentSceneIndex -= 1;

        if (previousSceneIndex < 0)
        {
            previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        }

        SceneManager.LoadScene(previousSceneIndex);
    }
}
