using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedBunny : MonoBehaviour
{
    private ScreenManager screenManager;
    private Animator animator;

    private RatGateAnimatorController ratGate;
    public bool isFree;
    private float timeToWait = 2.5f;

    private void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
        ratGate = FindObjectOfType<RatGateAnimatorController>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerWeapon" || collider.tag == "PlayerDownwardsWeapon")
        {
            //Debug.Log("You monster.");
            isFree = true;
            animator.SetBool("IsFree", true);
            StartCoroutine(WaitBeforeLoadingNextScene());
        }
    }

    private IEnumerator WaitBeforeLoadingNextScene()
    {
        yield return new WaitForSeconds(timeToWait);
        ratGate.OpenGate();
        //screenManager.LoadNextScene();
    }
}
