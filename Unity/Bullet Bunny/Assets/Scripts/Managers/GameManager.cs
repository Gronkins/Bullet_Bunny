using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private ScreenManager screenManager;
    private GameObject player;
    [SerializeField] private Vector3 checkpointPosition;
    public bool hasCheckpoint;
    public bool isPlayingGame;
    public float deaths;
    public float time;
    public int screenNumber;
    public int levelNumber;
    public int stageNumber;
    public bool isCounting;
    public bool isCarryingCollectible;
    public int carrotsCollected;
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
        player = GameObject.FindGameObjectWithTag("Player");
        Initialise();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        player = GameObject.FindGameObjectWithTag("Player");
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
        carrotsCollected = 0;
    }

    public void SetCheckpoint(Vector3 newCheckpointPosition)
    {
        hasCheckpoint = true;
        checkpointPosition = newCheckpointPosition;
    }

    public void RespawnAtCheckpoint()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.position = new Vector2 (5, 5);
        player.transform.position = checkpointPosition;
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

    public void ApplyScore()
    {
        if (isCarryingCollectible)
        {
            carrotsCollected += 1;
            isCarryingCollectible = false;
        }
    }
}
