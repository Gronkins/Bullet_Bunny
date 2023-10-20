using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private ScreenManager screenManager;
    public GameObject stageSelectMenu;
    public Button startingButton;
    
    // Start is called before the first frame update
    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    public void SetButton()
    {
        startingButton.Select();
    }

    public void LoadGame()
    {
        //GameManager.Instance.isPlayingGame = true;
        SceneManager.LoadScene(1);
        GameManager.Instance.isCounting = true;
    }

    public void StageSelect()
    {
        stageSelectMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
