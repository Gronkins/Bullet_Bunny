using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public BoxCollider2D boxCollider2D;
    public Animator animator;
    public LayerMask layerMask;
    public TrailRenderer trailRenderer;


    public Transform groundCheck;
    public float groundCheckRadius;
    public Vector2 groundCheckSize;
    public bool isGrounded;
    bool isJumping;
    
    [SerializeField]
    float movementSpeed = 10.0f;
    [SerializeField]
    float jumpHeight = 20.0f;

    [SerializeField]
    bool canDash = true;
    bool isDashing;

    public int maxDashes = 2;
    public int numOfDashes;
    [SerializeField]
    float dashStrength = 20.0f;
    float dashTime = 0.2f;
    float dashCooldown = 1.0f;

    public float horizontal;
    public float vertical;

    bool isFacingRight;

    Vector2 dashDirection;
    

    public float lastMoveHorizontal;
    //float lastMoveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();

        numOfDashes = maxDashes;
        lastMoveHorizontal = 1;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        horizontal = QuantizeAxis(Input.GetAxisRaw("Horizontal"));
        vertical = QuantizeAxis(Input.GetAxisRaw("Vertical"));
        //float vertical = Input.GetAxisRaw("Vertical");
        //float lastMoveHorizontal = Input.GetAxis("Horizontal");

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask); //Ground check with circle

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0f, layerMask); //Ground check with box

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (isDashing)
        {
            return;
        }

        if (QuantizeAxis(Input.GetAxisRaw("Horizontal")) == 1 || QuantizeAxis(Input.GetAxisRaw("Horizontal")) == -1)
        {
            //Records the last direction the player was facing such as left, right, up or down and sets the animation to that
            lastMoveHorizontal = QuantizeAxis(Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveHorizontal", Input.GetAxisRaw("Horizontal"));
        }

        if (isDashing == false)
        {
            rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, rigidBody2D.velocity.y);
            //rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);


            //rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y);
        }
        else
                if (isDashing == true)
                {
                    //rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
                    //rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y);
                    rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        }


        //rigidBody2D.velocity = new Vector2(horizontalm * movementSpeed, verticalm * movementSpeed);

        //movement.y = Input.GetAxisRaw("Vertical");
        //movement.x = Input.GetAxis("Horizontal");


        //Jump
        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            /*
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
            isJumping = true;
            animator.SetBool("IsJumping", true);
            */
            StartCoroutine(Jump());

            //rigidBody2D.velocity = new Vector2(vertical, jumpHeight);
        }

        //Sprint
        if (isGrounded == true && ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton7)) || (Input.GetAxis("Sprint") == 1)))
        {
            movementSpeed = 15.0f;
        }

        //Stop sprinting
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.JoystickButton7) || (Input.GetAxis("Sprint") == 0))
        {
            movementSpeed = 10.0f;
        }

        //Dash
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton0)) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (numOfDashes < 1)
        {
            canDash = false;
        }
        else
            canDash = true;

        if (isGrounded == true)
        {
            numOfDashes = maxDashes;
        }

        if(isGrounded == true)
        {
            isJumping = false;
        }
        
        if(isJumping == false)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private IEnumerator Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
        yield return new WaitForSeconds(0.1f);
        isJumping = true;
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(0.2f);
        //isJumping = false;


        yield return null;
    }
    
    private IEnumerator Dash()
    {
        //canDash = false;
        isDashing = true;
        animator.SetBool("IsDashing", true);
        trailRenderer.emitting = true;
        dashDirection = new Vector2(QuantizeAxis(Input.GetAxisRaw("Horizontal")), QuantizeAxis(Input.GetAxisRaw("Vertical")));
        float originalGravity = rigidBody2D.gravityScale;
        float originalVertical = QuantizeAxis(Input.GetAxisRaw("Vertical"));
        rigidBody2D.gravityScale = 0.0f;
        rigidBody2D.velocity = Vector2.zero;
        //rigidBody2D.velocity = new Vector2(transform.localScale.x * dashStrength, 0.0f);
        
        
        //rigidBody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * dashStrength;
        
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(lastMoveHorizontal, 0);
        }

        //rigidBody2D.velocity = new Vector2(horizontalm, verticalm).normalized * dashStrength;


        //rigidBody2D.velocity = dashDirection.normalized * dashStrength; old
        rigidBody2D.velocity = new Vector2(dashDirection.normalized.x * dashStrength, dashDirection.normalized.y * dashStrength);

        yield return new WaitForSeconds(dashTime);
        rigidBody2D.gravityScale = originalGravity;
        rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, originalVertical * movementSpeed);
        animator.SetBool("IsDashing", false);
        isDashing = false;
        trailRenderer.emitting = false;
        numOfDashes -= 1;
        yield return new WaitForSeconds(dashCooldown);
        //canDash = true;
    }
    
    void FixedUpdate()
    {
        if (lastMoveHorizontal > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        
        if (lastMoveHorizontal < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    int QuantizeAxis(float axis)
    {
        if (axis < -0.35f)
        {
            return -1;
        }
        
        if (axis > 0.35f)
        {
            return 1;
        }
        
        return 0;
    }
}
