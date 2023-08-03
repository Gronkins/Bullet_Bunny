using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotIconHUD : MonoBehaviour
{
    private GameObject player;
    public float interactRadius = 10f;

    private SpriteRenderer icon;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        icon = GetComponent<SpriteRenderer>();

        icon.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= interactRadius)
            {
                icon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                icon.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
