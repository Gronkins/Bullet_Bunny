using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalkerManager : MonoBehaviour
{
    public GameObject catWalker;
    public GameObject startingPoint;
    public GameObject endPoint;
    public bool hasSpawn;
    private GameObject catWalkerObject;
    public bool isFacingRight;
    public bool isFacingDown;
    
    private void Awake()
    {
        //catWalker.GetComponent<CatWalker>().Initialise(startingPoint, endPoint);
    }

    private void Start()
    {
        catWalkerObject = Instantiate(catWalker, startingPoint.transform.position, Quaternion.identity);
        catWalkerObject.GetComponent<CatWalker>().Initialise(startingPoint, endPoint, this);
        ApplyDirection();
        //Instantiate(catWalker, transform.position, Quaternion.identity);
        hasSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawn)
        {
            catWalkerObject = Instantiate(catWalker, startingPoint.transform.position, Quaternion.identity);
            catWalkerObject.GetComponent<CatWalker>().Initialise(startingPoint, endPoint, this);
            ApplyDirection();
            //Instantiate(catWalker, transform.position, Quaternion.identity);
            hasSpawn = true;
        }
    }

    private void ApplyDirection()
    {
        if (isFacingRight)
        {
            catWalkerObject.transform.localScale = new Vector3 (-1, 1, 1);
        }

        if (isFacingDown)
        {
            catWalkerObject.transform.localScale = new Vector3 (1, -1, 1);
        }

        if (isFacingRight && isFacingDown)
        {
            catWalkerObject.transform.localScale = new Vector3 (-1, -1, 1);
        }
    }
}
