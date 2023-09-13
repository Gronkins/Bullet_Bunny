using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        startingButton.Select();
    }
    
    public void SkipLevel()
    {
        screenManager.LoadNextScene();
        pauseMenu.SetButton();
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
