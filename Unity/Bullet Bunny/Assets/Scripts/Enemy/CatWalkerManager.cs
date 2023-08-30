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
    
    private void Awake()
    {
        //catWalker.GetComponent<CatWalker>().Initialise(startingPoint, endPoint);
    }

    private void Start()
    {
        catWalkerObject = Instantiate(catWalker, startingPoint.transform.position, Quaternion.identity);
        catWalkerObject.GetComponent<CatWalker>().Initialise(startingPoint, endPoint, this);
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
            //Instantiate(catWalker, transform.position, Quaternion.identity);
            hasSpawn = true;
        }
    }
}
