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
    public TextMeshProUGUI stageOneTime;
    public TextMeshProUGUI stageTwoTime;

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

        stageOneCarrots.text = "Carrots Collected " + GameManager.Instance.carrotsCollectedStageOne + " / 15";
        stageTwoCarrots.text = "Carrots Collected " + GameManager.Instance.carrotsCollectedStageTwo + " / 14";

        if (GameManager.Instance.bestTimeStageOne > 0)
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.bestTimeStageOne / 60f);
            int seconds = Mathf.FloorToInt(GameManager.Instance.bestTimeStageOne % 60f);
            int hundreths = Mathf.FloorToInt((GameManager.Instance.bestTimeStageOne * 100) % 100);
        
            stageOneTime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
        }

        if (GameManager.Instance.bestTimeStageTwo > 0)
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.bestTimeStageTwo / 60f);
            int seconds = Mathf.FloorToInt(GameManager.Instance.bestTimeStageTwo % 60f);
            int hundreths = Mathf.FloorToInt((GameManager.Instance.bestTimeStageTwo * 100) % 100);
        
            stageTwoTime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
        }
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
