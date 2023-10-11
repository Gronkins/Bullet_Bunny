using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    float respawnTime = 2.5f;
    public Sprite startingSprite;
    public Sprite greySprite;
    
    
    /*
    //Once the collectible is deactivated, it waits, then reactivates the collectible
    public void DeactivateCollectible(Collectible collectible)
    {
        collectible.gameObject.SetActive(false);

        StartCoroutine(RespawnCollectible(collectible));
    }
    */

    // Once the collectible is deactivated, it waits, then reactivates the collectible
    public void DeactivateCollectible(Collectible collectible)
    {
        BoxCollider2D boxCollider2D = collectible.GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = collectible.GetComponent<SpriteRenderer>();

        boxCollider2D.enabled = false;
        spriteRenderer.sprite = greySprite;


        //collectible.gameObject.SetActive(false);

        StartCoroutine(RespawnCollectible(collectible, boxCollider2D, spriteRenderer));
    }

    private IEnumerator RespawnCollectible(Collectible collectible, BoxCollider2D boxCollider2D, SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(respawnTime);

        boxCollider2D.enabled = true;
        spriteRenderer.sprite = startingSprite;

        //collectible.gameObject.SetActive(true);
    }
}
