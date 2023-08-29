using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class CatWalker : Enemy
{    
    private bool isArmed = false;
    public bool isWalking = false;
    private bool isStopped = false;
    public Transform startPoint;
    public Transform endPoint;
    public CatWalkerManager catWalkerManager;
    private MoveThenStop moveThenStop;
    
    private void Awake()
    {
        catWalkerManager = FindObjectOfType<CatWalkerManager>();
        moveThenStop = GetComponent<MoveThenStop>();
    }
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isArmed = false;
        animator.SetBool("IsArming", isArmed);
        animator.SetBool("IsAlive", true);
    }

    public void Initialise(GameObject newStartPoint, GameObject newEndPoint)
    {
        Debug.Log("Initialise called");
        startPoint = newStartPoint.transform;
        endPoint = newEndPoint.transform;
        moveThenStop.SetCatWalkerStart(startPoint, endPoint);
    }

    public void SetIsWalking()
    {
        isWalking = true;
        animator.SetBool("IsWalking", true);
    }

    public void SetIsStopped()
    {
        isStopped = true;
        animator.SetBool("IsStopped", true);
    }

    protected override void DealDamageToPlayer()
    {
        base.DealDamageToPlayer();
        
        if (isArmed)
        {
            isAlive = false;
            animator.SetBool("IsAlive", false);
        }
    }

    public void DestroySelf()
    {
        catWalkerManager.hasSpawn = false;
        Destroy(gameObject);
    }
}
