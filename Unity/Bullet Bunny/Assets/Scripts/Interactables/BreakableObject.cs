using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");

        if (collider.tag == "PlayerWeapon" || collider.tag == "PlayerDownwardsWeapon")
        {
            Hit();
        }
    }

    private void Hit()
    {
        Destroy(gameObject);
    }
}
