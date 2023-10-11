using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public Button startingButton;
    private MainMenu mainMenu;
    
    private void Awake()
    {
        mainMenu = FindObjectOfType<MainMenu>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            CloseBox();
        }
    }
    
    public void LoadStage1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadStage2()
    {
        SceneManager.LoadScene(22);
    }
    private void OnEnable()
    {
        startingButton.Select();
    }

    public void CloseBox()
    {
        mainMenu.SetButton();
        gameObject.SetActive(false);
    }
}
