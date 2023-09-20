using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubbleManager : MonoBehaviour
{
    public GameObject acidBubble;
    public float respawnTime;

    public void SetRespawnTimer()
    {
        StartCoroutine(RespawnBubble());
    }

    private IEnumerator RespawnBubble()
    {
        yield return new WaitForSeconds(respawnTime);
        acidBubble.SetActive(true);
        AcidBubbleEnemy acidBubbleEnemy = acidBubble.GetComponent<AcidBubbleEnemy>();
        acidBubbleEnemy.InitialiseBubble();
    }
}
