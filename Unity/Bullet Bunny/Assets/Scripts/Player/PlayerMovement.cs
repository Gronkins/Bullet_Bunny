using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public Rigidbody2D rigidBody2D;
    public BoxCollider2D boxCollider2D; //Terrain box collider, not used for Buck's hurtbox
    public Animator animator;
    public LayerMask layerMask; //This is the layermask for the terrain
    public LayerMask hazardLayerMask;
    public TrailRenderer trailRenderer;


    public Transform groundCheck;
    public float groundCheckRadius; //Left over from when the groundcheck was a sphere, it's kept in just cased we want to use a sphere for the ground check again
    public Vector2 groundCheckSize;
    public bool isGrounded;
    [SerializeField]
    bool isJumping;
    
    [SerializeField]
    float movementSpeed = 10.0f;
    [SerializeField]
    float jumpHeight = 16.0f;

    [SerializeField]
    bool canDash = true;
    bool isDashing;

    public int maxDashes = 2;
    public int numOfDashes;
    [SerializeField]
    float dashStrength = 20.0f;
    float dashTime = 0.2f;
    float dashCooldown = 0.1f; //Initial value was 1.0f

    //[SerializeField]
    float threshold = 0.15f; //0.35f is the initial value, later changed to 0.01f

    //Variables for collecting horizontal and vertical input
    public float horizontal;
    public float vertical;

    //Absolute horizontal means if there is any horizontal input at all, this variable will be possible
    public float absoluteHorizontal;

    bool isFacingRight;

    bool isAlive;

    [SerializeField]
    bool isAttacking;
    float attackTime = 0.15f;
    float attackCounter = 0.15f;

    Vector2 dashDirection;
    

    //Records the last direction the player was facing, useful for idling
    public float lastMoveHorizontal;
    //float lastMoveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        playerStats = GetComponent<PlayerStats>();

        //Initialising variables
        numOfDashes = maxDashes;
        lastMoveHorizontal = 1;
        isFacingRight = true;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Controls the player's death
        if(playerStats.playerHealth <= 0)
        {
            animator.SetBool("IsAlive", false);

            rigidBody2D.gravityScale = 0.0f; //Turns off gravity so Buck's explosion doesn't move
            rigidBody2D.velocity = Vector2.zero; //Sets veloicty to zero so you don't keep moving

            return;
        }
        
        horizontal = QuantizeAxis(Input.GetAxisRaw("Horizontal"));
        absoluteHorizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        vertical = QuantizeAxis(Input.GetAxisRaw("Vertical"));

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask); //Ground check with circle

        

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        animator.SetFloat("AbsoluteHorizontal", Mathf.Abs(absoluteHorizontal));

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0f, layerMask); //Ground check with box



        if (((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton4)) && !isAttacking))
        {
            StartCoroutine(Attack());
        }

        if (isAttacking)
        {
            //return;
        }



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
                rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, rigidBody2D.velocity.y); //movement code
                
            
             }
             else
                 if (isDashing == true)
                {

                }



        //Jump
        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Z)))
        {
            StartCoroutine(Jump());
        }

        //Dash
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canDash)
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
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }

        if (isGrounded == false)
        {
            animator.SetBool("IsJumping", true);
        }

    }

    private IEnumerator Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
        yield return new WaitForSeconds(0.05f);
        isJumping = true;
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(0.2f);


        yield return null;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.25f);
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        yield return null;
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        trailRenderer.emitting = true;
        dashDirection = new Vector2(horizontal, vertical);

        float originalGravity = rigidBody2D.gravityScale;
        float originalVertical = QuantizeAxis(Input.GetAxisRaw("Vertical"));
        rigidBody2D.gravityScale = 0.0f;
        rigidBody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.01f);
        numOfDashes -= 1;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(lastMoveHorizontal, 0);
        }

        rigidBody2D.velocity = new Vector2(dashDirection.normalized.x * dashStrength, dashDirection.normalized.y * dashStrength);

        yield return new WaitForSeconds(dashTime);
        rigidBody2D.gravityScale = originalGravity;
        rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, originalVertical * movementSpeed);
        animator.SetBool("IsDashing", false);
        isDashing = false;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
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
        if (axis < -threshold)
        {
            return -1;
        }
        
        if (axis > threshold)
        {
            return 1;
        }
        
        return 0;
    }
}
