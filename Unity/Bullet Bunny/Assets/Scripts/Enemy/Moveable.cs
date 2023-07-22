using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        //yOffset = 0.0251f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if (i == points.Length)
            {
                i = 0;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }
    
}
