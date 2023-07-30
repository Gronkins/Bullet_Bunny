using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecricityAnimatorController : MonoBehaviour
{
    private Animator animator;
    public ElectricityDirection electricityDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}

public enum ElectricityDirection
{
    Up,
    Down,
    Left,
    Right
}
