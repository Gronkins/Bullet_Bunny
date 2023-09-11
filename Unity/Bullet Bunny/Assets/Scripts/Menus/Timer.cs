using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(GameManager.Instance.time / 60f);
        int seconds = Mathf.FloorToInt(GameManager.Instance.time % 60f);
        int hundreths = Mathf.FloorToInt((GameManager.Instance.time * 100) % 100);
        
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundreths);
    }
}
