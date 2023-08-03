using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedBunny : MonoBehaviour
{
    private ScreenManager screenManager;
    private Animator animator;
    public bool isFree;
    public float timeToWait = 5f;

    private void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
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
            //screenManager.LoadNextScene();
        }
    }

    private IEnumerator WaitBeforeLoadingNextScene()
    {
        yield return new WaitForSeconds(timeToWait);
        screenManager.LoadNextScene();
    }
}
