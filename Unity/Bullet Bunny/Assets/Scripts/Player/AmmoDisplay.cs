using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("NumOfBullets", playerMovement.numOfDashes);
    }

    public void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; //Inverses the player's game object
        gameObject.transform.localScale = currentScale;
    }
}