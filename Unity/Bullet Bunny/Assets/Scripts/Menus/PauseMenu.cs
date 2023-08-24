using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    private string mainMenu = "MainMenu";

    private ScreenManager screenManager;
    
    private void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
        DontDestroyOnLoad(this);

        if (Instance != null & Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        StartCoroutine(WaitForUnpause());
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(mainMenu);
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SkipLevel()
    {
        screenManager.LoadNextScene();
        //Resume();
    }

    private IEnumerator WaitForUnpause()
    {
        Debug.Log("Test");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Test1");
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
