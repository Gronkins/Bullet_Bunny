using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    float movementSpeed = 10.0f;
    float jumpHeight = 20.0f;

    //Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, rigidBody2D.velocity.y);
        
        //movement.y = Input.GetAxisRaw("Vertical");
        //movement.x = Input.GetAxis("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
        }
    }

    void FixedUpdate()
    {
        //rigidBody2D.MovePosition(rigidBody2D.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
    }
}
