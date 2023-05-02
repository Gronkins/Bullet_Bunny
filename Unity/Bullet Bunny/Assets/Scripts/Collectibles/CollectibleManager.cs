using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public float respawnTime = 5.0f;

    private Collectible[] collectibles;
    
    // Start is called before the first frame update
    void Start()
    {
        collectibles = FindObjectsOfType<Collectible>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
