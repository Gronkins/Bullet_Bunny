using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatGate : MonoBehaviour
{
    public bool isFacingRight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isFacingRight)
        {
            FlipGameObject();
        }
    }

    private void FlipGameObject()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; //Inverses the player's game object
        gameObject.transform.localScale = currentScale;
    }
}
