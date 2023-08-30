using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformDustParticles : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool isFalling;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            spriteRenderer.enabled = true;
            animator.SetTrigger("Falling");
        }
    }
}
