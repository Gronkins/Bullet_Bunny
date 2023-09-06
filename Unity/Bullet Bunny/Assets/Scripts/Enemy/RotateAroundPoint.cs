using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public GameObject target;
    public float speed = 100f;

    private void Update() 
    {
        Quaternion rotation = transform.rotation;
        transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
