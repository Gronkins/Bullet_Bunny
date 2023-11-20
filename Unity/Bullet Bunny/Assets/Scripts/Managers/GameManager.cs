using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public PlayerMovement playerMovement;
    private ScreenManager screenManager;
    private CameraManager cameraManager;
    private SaveData saveData;
    private GameObject player;
    [SerializeField] private Vector3 checkpointPosition;
    private Dictionary<int, bool> checkpointDictionary = new Dictionary<int, bool>();
    public int currentStage;
    public bool hasCheckpoint;
    public bool isPlayingGame;
    public float deaths;
    public float time;
    public int levelNumber;
    public int stageNumber;

    // Save Data
    public int stageProgress;
    public int[] carrotsCollectedPerStage = new int[GameInformation.numberOfStages + 1];
    public float[] bestTimePerStage = new float[GameInformation.numberOfStages + 1];
    public float[] bestTimePerStageFullyCompleted = new float[GameInformation.numberOfStages + 1];
    public bool isCounting;
    public bool isCarryingCollectible;
    public int carrotsCollected;
    public PauseMenu pauseMenu;
    public bool isInDevMode;
    public bool isHoldingRight;
    
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
        cameraManager = FindObjectOfType<CameraManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        saveData = GetComponent<SaveData>();
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
        playerMovement = FindObjectOfType<PlayerMovement>();
        screenManager = FindObjectOfType<ScreenManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        Initialise();
        LoadGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isCounting)
        {
            time += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            isInDevMode = true;
        }

        if (isInDevMode)
        {
            if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Minus))
            {
                Debug.Log("Back");
                screenManager.LoadPreviousScene();
            }

            if(Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Equals))
            {
                Debug.Log("Forward");
                screenManager.LoadNextScene();
            }
        }
        /*
        if(Input.GetKeyDown(KeyCode.I))
        {
            SaveGame();
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            LoadGame();
        }
        */
    }
    public void Initialise()
    {
        isCounting = false;
        deaths = 0;
        isCarryingCollectible = false;
        carrotsCollected = 0;
        time = 0f;
        ClearAllCheckpoints();
    }

    public void ResetForNewLevel()
    {
        time = 0f;
        deaths = 0;
        carrotsCollected = 0;
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        if (!isInDevMode)
        {
            saveData.playerData.stageProgress = stageProgress;

            Array.Copy(carrotsCollectedPerStage, saveData.playerData.carrotsCollectedPerStage, carrotsCollectedPerStage.Length);
            Array.Copy(bestTimePerStage, saveData.playerData.bestTimePerStage, bestTimePerStage.Length);
            Array.Copy(bestTimePerStageFullyCompleted, saveData.playerData.bestTimePerStageFullyCompleted, bestTimePerStageFullyCompleted.Length);

            saveData.SaveToJson();
        }
    }

    public void LoadGame()
    {
        saveData.LoadFromJson();
        stageProgress = saveData.playerData.stageProgress;

        Array.Copy(saveData.playerData.carrotsCollectedPerStage, carrotsCollectedPerStage, saveData.playerData.carrotsCollectedPerStage.Length);
        Array.Copy(saveData.playerData.bestTimePerStage, bestTimePerStage, saveData.playerData.bestTimePerStage.Length);
        Array.Copy(saveData.playerData.bestTimePerStageFullyCompleted, bestTimePerStageFullyCompleted, saveData.playerData.bestTimePerStageFullyCompleted.Length);
    }

    public void SetCheckpoint(Vector3 newCheckpointPosition)
    {
        hasCheckpoint = true;
        checkpointPosition = newCheckpointPosition;
    }

    public void RespawnAtCheckpoint()
    {
        player.transform.position = checkpointPosition;
        cameraManager.MoveCamera();
    }

    public void SetCheckpointState(int checkpointID, bool isActive)
    {
        if (checkpointDictionary.ContainsKey(checkpointID))
        {
            checkpointDictionary[checkpointID] = isActive;
        }
        else
        {
            checkpointDictionary.Add(checkpointID, isActive);
        }
    }

    public bool GetCheckpointState(int checkpointID)
    {
        if (checkpointDictionary.ContainsKey(checkpointID))
        {
            return checkpointDictionary[checkpointID];
        }

        return false;
    }

    private void ClearAllCheckpoints()
    {
        checkpointDictionary.Clear();
    }

    public void GetLevelAndStageNumber()
    {
        Scene scene = SceneManager.GetActiveScene();
        int sceneNumber = scene.buildIndex;
        Debug.Log("Scene index = " + scene.buildIndex);

        if (sceneNumber < GameInformation.GetNumberOfLevelsInStage(1))
        {
            stageNumber = 1;
            levelNumber = sceneNumber;
        }
        else if (sceneNumber > GameInformation.GetNumberOfLevelsInStage(1))
        {
            stageNumber = 2;
            levelNumber = sceneNumber - GameInformation.GetNumberOfLevelsInStage(1);
        }
    }

    public void PlayerDeath()
    {
        StartCoroutine(screenManager.PlayerDeath());
    }

    public void ClearPauseMenu()
    {
        Debug.Log("Clear pause menu called");
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

    public void UpdateSaveGameData()
    {
        if (carrotsCollectedPerStage[currentStage] < carrotsCollected)
        {
            carrotsCollectedPerStage[currentStage] = carrotsCollected;
        }

        if (bestTimePerStage[currentStage] > time || bestTimePerStage[currentStage] == 0)
        {
            bestTimePerStage[currentStage] = time;
        }

        // If all carrots were collected in a particular stage, update the fully completed time
        if (carrotsCollected >= GameInformation.GetNumberOfCarrotsInStage(currentStage))
        {
            Debug.Log("All carrots collected in a stage");
            if (bestTimePerStageFullyCompleted[currentStage] > time || bestTimePerStageFullyCompleted[currentStage] == 0)
            {
                bestTimePerStageFullyCompleted[currentStage] = time;
            }
        }
    }
}
