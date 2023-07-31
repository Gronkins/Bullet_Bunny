using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : Enemy
{    
    private bool isArmed = false;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isArmed = false;
        animator.SetBool("IsArming", isArmed);
    }

    public void SetIsArming()
    {
        isArmed = true;
        animator.SetBool("IsArming", isArmed);
    }

    protected override void HandleEnemyCollision()
    {
        base.HandleEnemyCollision();
        
        if (isArmed)
        {
            EnemyDeath();
        }
    }
}
