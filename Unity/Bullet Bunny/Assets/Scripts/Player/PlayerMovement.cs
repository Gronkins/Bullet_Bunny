using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public Rigidbody2D rigidBody2D;

    public Vector3 boxSize; // Box size for the new ground check boxCast
    public float maxDistance; // Maximum distance for the box size for the boxCast (the Y direction)

    public BoxCollider2D boxCollider2D; //Terrain box collider, not used for Buck's hurtbox
    public Animator animator;
    public LayerMask layerMask; //This is the layermask for the terrain
    public LayerMask hazardLayerMask;
    public TrailRenderer trailRenderer;


    // These ground check variables are outdated, will be removed... later...
    public Transform groundCheck;
    public float groundCheckRadius; //Left over from when the groundcheck was a sphere, it's kept in just cased we want to use a sphere for the ground check again
    public Vector2 groundCheckSize; //Ground check for the square
    public bool isGrounded;
    [SerializeField]
    bool isJumping;
    public bool isOnPlatform;
    
    [SerializeField]
    float movementSpeed = 10.0f;
    [SerializeField]
    float jumpHeight = 16.0f;

    [SerializeField]
    bool canDash = true;
    bool isDashing;
    [SerializeField]
    float upwardsForce = 18.0f;

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
    public float absoluteVertical;

    bool isFacingRight;
    private float maximumFallVelocity = -25f;

    bool isAlive;

    [SerializeField]
    bool isAttacking;
    float attackTime = 0.15f;
    float attackCounter = 0.15f;

    public bool isEditingGizmos;
    public Vector3 yOffset;

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
            boxCollider2D.enabled = false;
            rigidBody2D.simulated = false;

            rigidBody2D.gravityScale = 0.0f; //Turns off gravity so Buck's explosion doesn't move
            rigidBody2D.velocity = Vector2.zero; //Sets veloicty to zero so you don't keep moving

            return;
        }

        //Collecting horizontal and vertical input
        horizontal = QuantizeAxis(Input.GetAxisRaw("Horizontal"));
        absoluteHorizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        absoluteVertical = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        vertical = QuantizeAxis(Input.GetAxisRaw("Vertical"));

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask); //Ground check with circle
        isGrounded = IsGrounded();

        
        //Setting animator variables
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        animator.SetFloat("AbsoluteHorizontal", Mathf.Abs(absoluteHorizontal));
        animator.SetFloat("AbsoluteVertical", Mathf.Abs(absoluteVertical));

        // isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0f, layerMask); // Outdated ground check code



        //Attack
        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton4)) && !isAttacking)
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

        //Collects horizontal input only if the plyaer is moving, useful for idling
        if (QuantizeAxis(Input.GetAxisRaw("Horizontal")) == 1 || QuantizeAxis(Input.GetAxisRaw("Horizontal")) == -1)
        {
            //Records the last direction the player was facing such as left or right and sets the animation to that
            lastMoveHorizontal = QuantizeAxis(Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveHorizontal", Input.GetAxisRaw("Horizontal"));
        }

            
        //The player can move like normal if they are not dashing
        if (isDashing == false)
        {
            rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, rigidBody2D.velocity.y); //Movement code

            if (rigidBody2D.velocity.y < maximumFallVelocity)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, maximumFallVelocity);
            }
        }
        else
            if (isDashing == true)
            {
                //No movement while dashing
            }



        //Jump
        
        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Z)))
        {
            //StartCoroutine(Jump());
        }
        

        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Z)))
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

        //Resets the number of dashes once the player touches the ground
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

    public void ApplyUpwardsForce()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, upwardsForce);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    // This is just so the new ground check boxCast can be seen in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isEditingGizmos)
        {
            Gizmos.DrawCube(transform.position - transform.up * maxDistance - yOffset, boxSize); // Code to draw the box cast for the ground check, uncomment if editing it
        }
    }
    
    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position - yOffset, boxSize, 0f, -transform.up, maxDistance, layerMask) || isOnPlatform)
        {
            // isGrounded = true;
            return true;
        }
        else
        {
            // isGrounded = false;
            return false;
        }
    }

    private IEnumerator Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
        yield return new WaitForSeconds(0.05f); //Waits for the player to actually get off the ground
        isJumping = true;
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(0.2f);

        yield return null;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        //rigidBody2D.isKinematic = true;
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.25f); //Waits for the attack animation to finish
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        //rigidBody2D.isKinematic = false;
        yield return null;
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        trailRenderer.emitting = true;
        dashDirection = new Vector2(horizontal, vertical); //Collects dash direction input

        float originalGravity = rigidBody2D.gravityScale; //Stores original gravity
        float originalVertical = QuantizeAxis(Input.GetAxisRaw("Vertical")); //Stores original vertical direction
        
        rigidBody2D.gravityScale = 0.0f; //Sets player gravity to zero, so they can dash in the air unaffected by gravity
        rigidBody2D.velocity = Vector2.zero; //Resets player velocity, so initial velocity doesn't have any strange interactions with the dash
        yield return new WaitForSeconds(0.01f);
        numOfDashes -= 1;

        //If there is no directional input the player will face in the direction they were last facing
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(lastMoveHorizontal, 0);
        }

        rigidBody2D.velocity = new Vector2(dashDirection.normalized.x * dashStrength, dashDirection.normalized.y * dashStrength); //Dash movement

        yield return new WaitForSeconds(dashTime); //Waits for dash to finish

        rigidBody2D.gravityScale = originalGravity; //Resets gravity to normal
        rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, originalVertical * movementSpeed); //Resets player's velocity
        animator.SetBool("IsDashing", false);
        isDashing = false;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
    }
    
    void FixedUpdate()
    {
        //Flips the player's sprite if they move
        if (lastMoveHorizontal > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        
        if (lastMoveHorizontal < 0 && isFacingRight)
        {
            FlipSprite();
        }
    }

    //Umm... Actually you are flipping the player's gameObject and not the sprite (nerd emoji)
    void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; //Inverses the player's game object
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    //Used in input collection, locks player's movement to 8 directions and is also used to create 'areas' for joystick input so it's more forgiving to input diagonals
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
