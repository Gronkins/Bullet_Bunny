using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletJumpParticlesController : MonoBehaviour
{
    private Animator animator;
    //private PlayerMovement playerMovement;
    private float horizontal;
    private float vertical;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //playerMovement = FindObjectOfType<PlayerMovement>();
    }
    
    public void Initialise(Vector2 dashDirection)
    {
        animator = GetComponent<Animator>();

        horizontal = dashDirection.x;
        vertical = dashDirection.y;
        //Debug.Log(horizontal + " and " + vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("AbsoluteHorizontal", Mathf.Abs(horizontal));
        animator.SetFloat("AbsoluteVertical", Mathf.Abs(vertical));
    }
    

    public void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; //Inverses the player's game object
        gameObject.transform.localScale = currentScale;
    }
}
