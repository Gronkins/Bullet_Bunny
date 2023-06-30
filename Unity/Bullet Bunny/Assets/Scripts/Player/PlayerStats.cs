using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerHealth = 1; //The player technically has health, but only 1, and everything deals 1 damage. Pretty much it's easier to manage the player dying this way

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //These are for testing
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //Debug.Log("Is touching");
        }

        if (collision.collider.tag == "Hazards")
        {
            //Debug.Log("Is touching");
        }
    }
}
