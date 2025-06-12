using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private CheckpointManager checkpointManager;
    public bool isActive;
    public int checkpointID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        checkpointManager = GetComponentInParent<CheckpointManager>();
    }

    private void Start()
    {
        if (checkpointManager == null && isActive)
        {
            animator.SetBool("IsActive", true);
        }
    }

    public void InitialiseCheckpoint()
    {
        if (isActive)
        {
            animator.SetBool("IsActive", true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        GameManager.Instance.SetCheckpoint(gameObject.transform.position);
        animator.SetTrigger("Activate");
        isActive = true;
        GameManager.Instance.SetCheckpointState(checkpointID, isActive);
        Debug.Log("Player has checkpoint!");
    }
}
