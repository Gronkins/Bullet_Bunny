using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedBunny : MonoBehaviour
{
    private ScreenManager screenManager;

    private void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerWeapon" || collider.tag == "PlayerDownwardsWeapon")
        {
            Debug.Log("You monster.");
            screenManager.LoadNextScene();
        }
    }
}
