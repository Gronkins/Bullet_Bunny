using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private ScreenManager screenManager;
    public bool isPlayingGame;
    public float deaths;
    public float time;
    public int screenNumber;
    public int levelNumber;
    public int stageNumber;
    public bool isCounting;
    public int score;
    public PauseMenu pauseMenu;
    
    private void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        screenManager = FindObjectOfType<ScreenManager>();
        Initialise();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            time += Time.deltaTime;
        }
    }

    private void Initialise()
    {
        deaths = 0;
    }

    public void GetLevelNumber()
    {
        Scene scene = SceneManager.GetActiveScene();
        int number = scene.buildIndex;

        if (number <= 20)
        {
            levelNumber = 1;
            stageNumber = number;
        }
        else if (number > 20)
        {
            levelNumber = 2;
            stageNumber = number - 21;
        }
    }

    public void PlayerDeath()
    {
        StartCoroutine(screenManager.PlayerDeath());
    }

    public void ClearPauseMenu()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        Destroy(pauseMenu.gameObject);
    }
}
