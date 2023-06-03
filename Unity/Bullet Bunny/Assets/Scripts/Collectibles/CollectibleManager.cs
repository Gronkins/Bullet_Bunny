using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    float respawnTime = 2.5f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Once the collectible is deactivated, it waits, then reactivates the collectible
    public void DeactivateCollectible(Collectible collectible)
    {
        collectible.gameObject.SetActive(false);

        StartCoroutine(RespawnCollectible(collectible));
    }

    private IEnumerator RespawnCollectible(Collectible collectible)
    {
        yield return new WaitForSeconds(respawnTime);

        collectible.gameObject.SetActive(true);
    }
}
