using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThenStop : MonoBehaviour
{
    public float speed;
    public Transform startingPoint;
    public Transform endPoint;
    private EnemyMine enemyMine;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint.position;
        enemyMine = GetComponent<EnemyMine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, endPoint.position) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        }
        else
        {
            if (enemyMine != null)
            {
                enemyMine.SetIsArming();
            }
        }
    }
    
}
