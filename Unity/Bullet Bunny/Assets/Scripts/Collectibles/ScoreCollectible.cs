using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    private Animator animator;
    private bool isCollectible;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isCollectible = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Touch");
            if (isCollectible)
            {
                isCollectible = false;
                animator.SetTrigger("Collect");
                GameManager.Instance.isCarryingCollectible = true;
            }
            //GameManager.Instance.score += 1;
            //Destroy(gameObject);
        }
    }
}
