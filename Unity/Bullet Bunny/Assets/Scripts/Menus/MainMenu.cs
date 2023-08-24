using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private ScreenManager screenManager;
    
    // Start is called before the first frame update
    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
