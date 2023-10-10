using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject carrotIcon;
    
    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(GameManager.Instance.time / 60f);
        int seconds = Mathf.FloorToInt(GameManager.Instance.time % 60f);
        int hundreths = Mathf.FloorToInt((GameManager.Instance.time * 100) % 100);
        
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);

        if (GameManager.Instance.isCarryingCollectible)
        {
            DisplayCarrotIcon();
        }
        else
        {
            HideCarrotIcon();
        }
    }

    public void DisplayCarrotIcon()
    {
        carrotIcon.SetActive(true);
    }

    public void HideCarrotIcon()
    {
        carrotIcon.SetActive(false);
    }
}
