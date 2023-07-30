using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedElectricFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ElecricityAnimatorController[] electricities = GetComponentsInChildren<ElecricityAnimatorController>();

        foreach (ElecricityAnimatorController elecricity in electricities)
        {
            elecricity.DisableGameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
