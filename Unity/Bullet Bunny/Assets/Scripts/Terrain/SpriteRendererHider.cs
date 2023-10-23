using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererHider : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        spriteRender.material.color = new Color(1f, 1f, 1f, 0f);
    }

}
