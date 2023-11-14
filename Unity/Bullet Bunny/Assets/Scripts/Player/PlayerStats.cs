using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject sideHitbox;
    public GameObject upHitbox;
    public GameObject downHitbox;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
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

    public void TakeDamage()
    {
        //playerHealth -= 1;
        isDead = true;
        GameManager.Instance.PlayerDeath();
        //GameManager.Instance.deaths += 1;
    }
}
