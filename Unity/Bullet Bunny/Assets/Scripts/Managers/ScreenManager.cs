using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(512, 288, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SetScreenSize();
            Debug.Log("Screen size");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void SetScreenSize()
    {
        bool fullscreen = Screen.fullScreen;
        int width = 512;
        int height = 288;
        Screen.SetResolution(width, height, fullscreen);
    }
}
