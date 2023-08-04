using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatGate : MonoBehaviour
{
    private Animator animator;
    private CagedBunny cagedBunny;

    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        cagedBunny = FindObjectOfType<CagedBunny>();
    }

    public void OpenGate()
    {
        animator.SetBool("IsOpen", true);
    }
}
