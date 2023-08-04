using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatGateCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.transform.position.y > transform.position.y)
        if (collision.collider.CompareTag("Player"))
        {
            if(transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //if(collision.transform.position.y > transform.position.y)
        if (collision.collider.CompareTag("Player"))
        {
            if(transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
