using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : Enemy
{
    private PlayerStats newPlayerStats;
    private PlayerMovement newPlayerMovement;
    public GameObject target;
    public float speed = 100f;

    private void Update() 
    {
        Quaternion rotation = transform.rotation;
        transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
