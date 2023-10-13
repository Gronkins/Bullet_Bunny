using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private float respawnTime = 2.5f;

    // Once the collectible is deactivated, it waits, then reactivates the collectible
    public void DeactivateCollectible(Collectible collectible)
    {
        BoxCollider2D boxCollider2D = collectible.GetComponent<BoxCollider2D>();
        Animator animator = collectible.GetComponent<Animator>();

        boxCollider2D.enabled = false;
        animator.SetTrigger("Pickup");

        StartCoroutine(RespawnCollectible(boxCollider2D, animator));
    }

    private IEnumerator RespawnCollectible(BoxCollider2D boxCollider2D, Animator animator)
    {
        yield return new WaitForSeconds(respawnTime);

        boxCollider2D.enabled = true;
        animator.SetTrigger("Refresh");
    }
}
