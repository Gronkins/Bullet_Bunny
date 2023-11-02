using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class StageSelect : MonoBehaviour
{
    public Button startingButton;
    private MainMenu mainMenu;
    public Image image;
    public TextMeshProUGUI stageOneCarrots;
    public TextMeshProUGUI stageTwoCarrots;

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

        stageOneCarrots.text = "Carrots Collected " + GameManager.Instance.carrotsCollectedStageOne + " / 13";
        stageTwoCarrots.text = "Carrots Collected " + GameManager.Instance.carrotsCollectedStageTwo + " / 13";
    }

    private void Initialise()
    {
        if (GameManager.Instance.stageProgress > 0 || GameManager.Instance.isInDevMode)
        {
            if (image != null)
            {
                Destroy(image.gameObject);
            }
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
