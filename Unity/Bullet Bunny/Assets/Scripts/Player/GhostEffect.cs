using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public GameObject ghost;
    public float ghostDelay;
    private float ghostDelayTimer;
    private PlayerMovement playerMovement;
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        ghostDelayTimer = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.dashTimer > 0)
        {
            if (ghostDelayTimer > 0)
            {
                ghostDelayTimer -= Time.deltaTime;
            }
            else
            {
                GameObject ghostObject = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                ghostObject.transform.localScale = this.transform.localScale;
                ghostObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelayTimer = ghostDelay;
                Destroy(ghostObject, 0.25f);
            }
        }
    }
}
