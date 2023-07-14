using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.material.color = new Color(1f, 1f, 1f, 0f); //Hides the red hazards in-game
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
