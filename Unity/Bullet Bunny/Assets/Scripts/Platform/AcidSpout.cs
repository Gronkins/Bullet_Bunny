using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpout : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float timeBeforeStart;
    public float intervalDelay;
    private bool hasFinished;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeStart());
        spriteRenderer.enabled = false;
        hasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFinished)
        {
            StartCoroutine(WaitForInterval());
        }
    }

    public void SetHasFinished()
    {
        hasFinished = true;
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        spriteRenderer.enabled = true;
        animator.SetTrigger("Start");
    }

    private IEnumerator WaitForInterval()
    {
        hasFinished = false;
        yield return new WaitForSeconds(intervalDelay);
        animator.SetTrigger("Start");
    }
}
