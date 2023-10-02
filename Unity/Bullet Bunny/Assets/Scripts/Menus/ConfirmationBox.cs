using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmationBox : MonoBehaviour
{
    public Button startingButton;
    private ScreenManager screenManager;
    public PauseMenu pauseMenu;

    private void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    private void Start()
    {
        pauseMenu = GetComponentInParent<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            CloseBox();
        }
    }

    private void OnEnable()
    {
        startingButton.Select();
    }
    
    public void SkipLevel()
    {
        screenManager.LoadNextScene();
        pauseMenu.SetButton();

        Scene scene = SceneManager.GetActiveScene();
        
        if (scene.name == "19")
        {
            PauseMenu.isPaused = false;
            Destroy(pauseMenu.gameObject);
        }

        if (scene.name == "CatLevel15")
        {
            PauseMenu.isPaused = false;
            Destroy(pauseMenu.gameObject);
        }
        gameObject.SetActive(false);
        pauseMenu.GetStageNumberPlus1();
        //Resume();
    }

    public void CloseBox()
    {
        pauseMenu.SetButton();
        gameObject.SetActive(false);
    }
}
