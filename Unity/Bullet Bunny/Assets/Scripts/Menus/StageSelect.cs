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
    public TextMeshProUGUI stageOneTimeFullyComplete;
    public TextMeshProUGUI stageTwoTimeFullyComplete;

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

        /*
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
        */
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

        UpdateMenu();
    }

    private void UpdateMenu()
    {
        UpdateCarrotsText(stageOneCarrots, 1);
        UpdateCarrotsText(stageTwoCarrots, 2);

        UpdateTimeText(stageOneTime, 1);
        UpdateTimeText(stageTwoTime, 2);

        UpdateTimeTextFullyComplete(stageOneTimeFullyComplete, 1);
        UpdateTimeTextFullyComplete(stageTwoTimeFullyComplete, 2);
    }

    private void UpdateCarrotsText(TextMeshProUGUI text, int stageNumber)
    {
        text.text = "Carrots Collected " + GameManager.Instance.carrotsCollectedPerStage[stageNumber] + " / " + GameInformation.GetNumberOfCarrotsInStage(stageNumber);
    }

    private void UpdateTimeText(TextMeshProUGUI text, int stageNumber)
    {
        if (GameManager.Instance.bestTimePerStage[stageNumber] > 0)
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.bestTimePerStage[stageNumber] / 60f);
            int seconds = Mathf.FloorToInt(GameManager.Instance.bestTimePerStage[stageNumber] % 60f);
            int hundreths = Mathf.FloorToInt((GameManager.Instance.bestTimePerStage[stageNumber] * 100) % 100);

            text.text = "Best Time\n" + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
        }
        else
        {
            text.text = "Best Time\n--:--:--";
        }
    }

    private void UpdateTimeTextFullyComplete(TextMeshProUGUI text, int stageNumber)
    {
        if (GameManager.Instance.bestTimePerStageFullyCompleted[stageNumber] > 0)
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.bestTimePerStageFullyCompleted[stageNumber] / 60f);
            int seconds = Mathf.FloorToInt(GameManager.Instance.bestTimePerStageFullyCompleted[stageNumber] % 60f);
            int hundreths = Mathf.FloorToInt((GameManager.Instance.bestTimePerStageFullyCompleted[stageNumber] * 100) % 100);

            text.text = "Best Time (100%)\n" + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
        }
        else
        {
            text.text = "Best Time (100%)\n--:--:--";
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
