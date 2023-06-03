using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class was used for testing
public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Level00");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attempt to load scene");
            SceneManager.LoadScene("Level00");
            Debug.Log("Scene loaded.");
        }
    }
}