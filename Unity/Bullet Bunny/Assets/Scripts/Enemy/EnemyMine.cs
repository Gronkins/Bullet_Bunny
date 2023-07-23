using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : Enemy
{    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator.SetBool("IsArming", false);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("IsMoving", false);
    }

    public void SetIsArming()
    {
        animator.SetBool("IsArming", true);
    }
}
