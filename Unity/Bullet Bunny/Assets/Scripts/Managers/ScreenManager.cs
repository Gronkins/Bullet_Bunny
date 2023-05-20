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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadPreviousScene();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerStats.playerHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int previousSceneIndex = currentSceneIndex -= 1;

        if (previousSceneIndex < 0)
        {
            previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        }

        SceneManager.LoadScene(previousSceneIndex);
    }
}
