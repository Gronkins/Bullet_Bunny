using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecricityAnimatorController : MonoBehaviour
{
    private Animator animator;
    public ElectricityDirection electricityDirection;
    private BoxCollider2D boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetAnimation();
    }

    private void SetAnimation()
    {
        switch (electricityDirection)
        {
            case ElectricityDirection.Up:
                animator.SetBool("IsUp", true);
                break;
            case ElectricityDirection.Down:
                animator.SetBool("IsDown", true);
                break;
            case ElectricityDirection.Left:
                animator.SetBool("IsLeft", true);
                break;
            case ElectricityDirection.Right:
                animator.SetBool("IsRight", true);
                break;
            default:
                break;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
        //Gizmos.DrawSphere(transform.position, 1);
    }
}

public enum ElectricityDirection
{
    Up,
    Down,
    Left,
    Right
}