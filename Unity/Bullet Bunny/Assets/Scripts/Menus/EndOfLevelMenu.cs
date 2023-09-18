using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class EndOfLevelMenu : MonoBehaviour
{
    //public static PauseMenu Instance;
    
    public static bool isPaused = false;
    public Button startingButton;
    public GameObject pauseMenuUI;
    public GameObject checkBox;
    public TextMeshProUGUI deathText;
    private string mainMenu = "MainMenu";

    private ScreenManager screenManager;
    
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
        //deathText.text = "x " + GameManager.Instance.deaths;
        /*
        if (GameManager.Instance.deaths == 0)
        {
            deathText.text = "000";
        }

        if (GameManager.Instance.deaths > 0 && GameManager.Instance.deaths < 10)
        {
            deathText.text = "00" + GameManager.Instance.deaths;
        }

        if (GameManager.Instance.deaths < 100)
        {
            deathText.text = "0" + GameManager.Instance.deaths;
        }


        if (GameManager.Instance.deaths >= 100)
        {
            deathText.text = GameManager.Instance.deaths.ToString();
        }
        */

        //if ()
        
        deathText.text = "You died " + GameManager.Instance.deaths + " times";

        isPaused = true;
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        GameManager.Instance.isPlayingGame = false;
        GameManager.Instance.time = 0f;
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(mainMenu);
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SetButton()
    {
        startingButton.Select();
    }

    public void SkipLevel()
    {
        //screenManager.LoadNextScene();
        checkBox.SetActive(true);
        //GetStageNumber();
        //Resume();
    }

    private IEnumerator WaitForUnpause()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
