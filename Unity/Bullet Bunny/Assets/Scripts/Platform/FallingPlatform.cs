using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private FallingPlatformController controller;
    public float timeBeforeFall = 0.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        controller = GetComponentInParent<FallingPlatformController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Player touched");
            controller.SetFalling();
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        yield return new WaitForSeconds(timeBeforeFall);
        animator.SetTrigger("Falling");
        //controller.SetFalling();
        StartCoroutine(Fall());
    }
    
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(1);
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidBody2D.mass = 1000f;
        rigidBody2D.gravityScale = 2f;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
