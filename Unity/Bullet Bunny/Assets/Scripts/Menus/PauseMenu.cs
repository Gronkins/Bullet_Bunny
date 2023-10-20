using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    
    public static bool isPaused = false;
    public Button startingButton;
    public GameObject pauseMenuUI;
    public GameObject checkBox;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI screenAndLevelText;
    public TextMeshProUGUI scoreText;
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
        if (GameManager.Instance.deaths < 10)
        {
            deathText.text = "00" + GameManager.Instance.deaths;
        }
        else if (GameManager.Instance.deaths > 9 && GameManager.Instance.deaths < 100)
        {
            deathText.text = "0" + GameManager.Instance.deaths;
        }
        else if (GameManager.Instance.deaths > 99)
        {
            deathText.text = GameManager.Instance.deaths.ToString();
        }

        if (GameManager.Instance.carrotsCollected < 10)
        {
            scoreText.text = "x 0" + GameManager.Instance.carrotsCollected;
        }
        else if (GameManager.Instance.carrotsCollected > 9)
        {
            scoreText.text = "x " + GameManager.Instance.deaths.ToString();
        }

        GetStageNumber();
        isPaused = true;
    }

    public void GetStageNumber()
    {
        Scene scene = SceneManager.GetActiveScene();
        //int number = scene.buildIndex;
        GameManager.Instance.GetLevelNumber();
        screenAndLevelText.text = GameManager.Instance.levelNumber + " - " + GameManager.Instance.stageNumber;
    }

    public void GetStageNumberPlus1()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameManager.Instance.GetLevelNumber();
        int number = GameManager.Instance.stageNumber + 1;
        screenAndLevelText.text = GameManager.Instance.levelNumber + " - " + number;
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //GameManager.Instance.isPlayingGame = false;
        //GameManager.Instance.time = 0f;
        GameManager.Instance.Initialise();
        //Destroy(GameManager.Instance.gameObject);
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
