using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    private ScreenManager screenManager;
    public bool isPlayingGame;
    public float deaths;
    public float time;
    public int screenNumber;
    public int levelNumber;
    
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
        //time = 5990;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    private void Initialise()
    {
        deaths = 0;
    }

    public void PlayerDeath()
    {
        StartCoroutine(screenManager.PlayerDeath());
    }
}
