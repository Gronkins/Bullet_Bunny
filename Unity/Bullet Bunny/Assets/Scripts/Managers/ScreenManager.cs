using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    PlayerStats playerStats;
    private bool hasDied;
    
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(512, 288, true);
        playerStats = FindObjectOfType<PlayerStats>();
        //Application.targetFrameRate = 30;
        hasDied = false;
        //playerStats = player.GetComponent<PlayerStats>();
    }

    //Waits for a moment, then restarts the current scene
    public IEnumerator PlayerDeath()
    {
        GameManager.Instance.isCarryingCollectible = false;

        if (!hasDied)
        {
            hasDied = true;
            GameManager.Instance.deaths += 1;
        }

        Debug.Log("Death called");
        Destroy(playerStats.upHitbox);
        Destroy(playerStats.sideHitbox);
        Destroy(playerStats.downHitbox);
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return null;
        playerStats.isDead = false;
        hasDied = false;

        if (GameManager.Instance.hasCheckpoint)
        {
            GameManager.Instance.RespawnAtCheckpoint();
        }

        //hasDied = false;
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
        GameManager.Instance.ApplyScore();
        GameManager.Instance.hasCheckpoint = false;
        
        //Loads the next scene, if at the final scene, goes back to the first one
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex += 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadEndOfLevelScreen()
    {
        GameManager.Instance.ApplyScore();
        GameManager.Instance.hasCheckpoint = false;
        GameManager.Instance.UpdateSaveGameData();
        
        if (SceneManager.GetActiveScene().buildIndex <= GameInformation.GetNumberOfLevelsInStage(1))
        {
            GameManager.Instance.stageProgress = 1;
            SceneManager.LoadScene("EndOfLevelScreen");
        }
        else
        {
            SceneManager.LoadScene("EndOfGameScreen");
        }

        GameManager.Instance.SaveGame();
    }

    public void LoadPreviousScene()
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
