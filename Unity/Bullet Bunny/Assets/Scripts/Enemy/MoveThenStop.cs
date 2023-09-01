using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThenStop : MonoBehaviour
{
    public float speed;
    public Transform startingPoint;
    public Transform endPoint;
    private EnemyMine enemyMine;
    private CatWalker catWalker;
    private AcidBubbleEnemy acidBubbleEnemy;
    
    /*
    private void Awake()
    {
        catWalker = GetComponent<CatWalker>();
        enemyMine = GetComponent<EnemyMine>();

        if (catWalker != null)
        {
            startingPoint = catWalker.startPoint;
        }
        else
        {
            transform.position = startingPoint.position;
        }
    }
    */
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint.position;
        enemyMine = GetComponent<EnemyMine>();
        catWalker = GetComponent<CatWalker>();
        acidBubbleEnemy = GetComponent<AcidBubbleEnemy>();

        if (catWalker != null)
        {
            transform.position = catWalker.startPoint.position;
        }
    }

    public void SetCatWalkerStart(Transform startPoint, Transform newEndPoint)
    {
        startingPoint = startPoint;
        transform.position = startPoint.position;
        endPoint = newEndPoint;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMine != null)
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
        
        if (catWalker != null && catWalker.isWalking)
        {
            endPoint = catWalker.endPoint;
            
            if (Vector2.Distance(transform.position, endPoint.position) > 0.02f)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            }
            else
            {
                if (catWalker != null)
                {
                    catWalker.SetIsStopped();
                }
            }
        }

        if (acidBubbleEnemy != null)
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
    
}
