using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            GameManager.Instance.SetCheckpoint(gameObject.transform.position);
            Debug.Log("Player has checkpoint!");
        }
    }
}
