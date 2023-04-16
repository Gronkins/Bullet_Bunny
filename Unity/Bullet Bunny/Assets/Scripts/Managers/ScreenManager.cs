using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    PlayerStats playerStats;
    GameObject player;
    
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SetScreenSize();
            Debug.Log("Screen size");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerStats.playerHealth <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SetScreenSize()
    {
        bool fullscreen = Screen.fullScreen;
        int width = 512;
        int height = 288;
        Screen.SetResolution(width, height, fullscreen);
    }
}
