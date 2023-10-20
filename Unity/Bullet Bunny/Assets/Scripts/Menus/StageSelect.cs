using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StageSelect : MonoBehaviour
{
    public Button startingButton;
    private MainMenu mainMenu;
    public Image image;
    
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

    private void Initialise()
    {
        if (GameManager.Instance.stageProgress > 0 || GameManager.Instance.isInDevMode)
        {
            Destroy(image.gameObject);
        }
    }
    
    public void LoadStage1()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.isCounting = true;
    }

    public void LoadStage2()
    {
        if (GameManager.Instance.stageProgress > 0 || GameManager.Instance.isInDevMode)
        {
            SceneManager.LoadScene(22);
            GameManager.Instance.isCounting = true;
        }
    }
    private void OnEnable()
    {
        startingButton.Select();
        Initialise();
    }

    public void CloseBox()
    {
        mainMenu.SetButton();
        gameObject.SetActive(false);
    }
}
