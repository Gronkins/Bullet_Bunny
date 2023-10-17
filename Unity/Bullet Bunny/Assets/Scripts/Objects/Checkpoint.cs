using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (GameManager.Instance.hasCheckpoint)
        {
            animator.SetBool("IsActive", true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            GameManager.Instance.SetCheckpoint(gameObject.transform.position);
            animator.SetTrigger("Activate");
            Debug.Log("Player has checkpoint!");
        }
    }
}
