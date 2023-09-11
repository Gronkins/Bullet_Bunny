using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public bool isPlayingGame;
    public float deaths;
    public float time;
    
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

        Initialise();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
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
}
