using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public float yOffset;
    public Transform[] points;
    private PlayerMovement playerMovement;

    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        playerMovement = FindObjectOfType<PlayerMovement>();
        //yOffset = 0.0251f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if (i == points.Length)
            {
                i = 0;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.transform.position.y > transform.position.y)
        if (collision.collider.CompareTag("Player"))
        {
            if(transform.position.y < collision.transform.position.y - yOffset)
            {
                collision.transform.SetParent(transform);
                playerMovement.isOnPlatform = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //if(collision.transform.position.y > transform.position.y)
        if (collision.collider.CompareTag("Player"))
        {
            if(transform.position.y < collision.transform.position.y - yOffset /*&& !(GameManager.Instance.playerMovement.rigidBody2D.velocity.y > 0)*/)
            {
                collision.transform.SetParent(transform);
                playerMovement.isOnPlatform = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            playerMovement.isOnPlatform = false;
        }
    }
    
}
