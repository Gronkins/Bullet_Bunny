using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isSliding;
    public bool hasSlideSpeed;
    private bool isTouchingSliding;
    [SerializeField] private float notTouchingSlidingTimer = 0f;
    private float notTouchingSlidingDuration = 4f;
    
    public PlayerStats playerStats;
    
    public Rigidbody2D rigidBody2D;

    public Vector3 boxSize; // Box size for the new ground check boxCast
    public Vector3 jumpableBoxSize; // Box size for the new ground check boxCast
    public float maxDistance; // Maximum distance for the box size for the boxCast (the Y direction)

    public BoxCollider2D boxCollider2D; //Terrain box collider, not used for Buck's hurtbox
    public Animator animator;
    public LayerMask layerMask; //This is the layermask for the terrain
    public GameObject bulletJumpParticles;

    public bool isGrounded;
    [SerializeField]
    bool isJumping;
    public int numOfJumps = 1;
    public int maxJumps = 1;
    public bool isOnPlatform;
    public float coyoteTime;
    public float coyoteTimeDuration = 0.3f;
    
    [SerializeField]
    float movementSpeed = 10.0f;
    [SerializeField]
    float jumpHeight = 16.0f;

    [SerializeField]
    bool canDash = true;
    bool isDashing;
    [SerializeField]
    float upwardsForce = 16.0f;

    public float slideSpeed = 1.2f;

    private bool canBulletJump;
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

    public bool isEditingGizmos;
    public Vector3 yOffset;

    Vector2 dashDirection;

    private TutorialCharacter tutorialCharacter;
    private AmmoDisplay ammoDisplay;

    private IEnumerator slideCorotine;

    public float forwardSlideMultiplier = 1.25f;
    public float backwardsSlideMultipler = 0.75f;
    

    //Records the last direction the player was facing, useful for idling
    public float lastMoveHorizontal;
    //float lastMoveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerStats = GetComponent<PlayerStats>();
        tutorialCharacter = GetComponent<TutorialCharacter>();
        ammoDisplay = GetComponentInChildren<AmmoDisplay>();

        //Initialising variables
        numOfDashes = maxDashes;
        lastMoveHorizontal = 1;
        isFacingRight = true;
        isAlive = true;
        canBulletJump = true;

        if (tutorialCharacter != null)
        {
            animator.runtimeAnimatorController = tutorialCharacter.tutorialAnimatorController;
            canBulletJump = false;
            ammoDisplay.gameObject.SetActive(false);
        }

        slideCorotine = EndSlidingMomentum();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.isPaused)
        {
            return;
        }
        
        //Controls the player's death
        if(playerStats.isDead)
        {
            animator.SetBool("IsAlive", false);
            ammoDisplay.gameObject.SetActive(false);
            boxCollider2D.enabled = false;
            rigidBody2D.simulated = false;

            rigidBody2D.gravityScale = 0.0f; //Turns off gravity so Buck's explosion doesn't move
            rigidBody2D.velocity = Vector2.zero; //Sets veloicty to zero so you don't keep moving

            return;
        }

        if (!isTouchingSliding)
        {
            notTouchingSlidingTimer += Time.deltaTime;

            if (notTouchingSlidingTimer >= notTouchingSlidingDuration)
            {
                //StartCoroutine(EndSlidingMomentum());
                EndSlideForce();
            }
        }

        if(!IsGrounded())
        {
            coyoteTime += Time.deltaTime;
        }
        else
        {
            coyoteTime = 0f;
        }
        
        if (hasSlideSpeed)
        {
            ApplySlideForce();
        }

        if(isSliding)
        {
            //StopCoroutine(slideCorotine);
            //StopCoroutine(EndSlidingMomentum());
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
        animator.SetBool("IsSliding", isSliding);

        // isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0f, layerMask); // Outdated ground check code

        if (isSliding)
        {
            if (!isFacingRight)
            {
                FlipSprite();
            }
        }

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
        if (isDashing == false && !hasSlideSpeed)
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
        
        if (numOfJumps > 0 && CanJump() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Z)))
        {
            StartCoroutine(Jump());
        }

        if (numOfDashes < 1)
        {
            canDash = false;
        }
        else
            canDash = true;

        //Dash
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canDash  && canBulletJump)
        {
            dashDirection = new Vector2(horizontal, vertical); //Collects dash direction input

            //StartCoroutine(Dash());

            if (CanBulletJump())
            {
                StartCoroutine(Dash());
            }
        }

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

    private void ApplySlideForce()
    {
        //if (isJumping)
        //{
            //rigidBody2D.velocity = new Vector2(movementSpeed * 0.5f, jumpHeight);
        //}
        //else
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            rigidBody2D.velocity = new Vector2(movementSpeed * slideSpeed * forwardSlideMultiplier, rigidBody2D.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            rigidBody2D.velocity = new Vector2(movementSpeed * slideSpeed * backwardsSlideMultipler, rigidBody2D.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            rigidBody2D.velocity = new Vector2(movementSpeed * slideSpeed, rigidBody2D.velocity.y);
        }
    }

    private bool CanJump()
    {
        if (IsGrounded())
        {
            return true;
        }

        if (IsInJumpableArea())
        {
            return true;
        }

        if (coyoteTime <= coyoteTimeDuration)
        {
            return true;
        }

        return false;
    }

    private bool CanBulletJump()
    {
        if (isTouchingSliding) // was if (isSliding)
        {
            if(dashDirection == new Vector2(0, 0) || dashDirection == new Vector2(-1, 0) || dashDirection == new Vector2(1, 0) || vertical == -1 || (dashDirection == new Vector2(0, -1) && isGrounded))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (!(dashDirection == new Vector2(0, -1) && isGrounded))
        {
            return true;
        }

        return false;
    }

    public void PickUpGun()
    {
        if (tutorialCharacter != null)
        {
            animator.runtimeAnimatorController = tutorialCharacter.originalAnimatorController;
            Destroy(tutorialCharacter);
            ammoDisplay.enabled = true;
            canBulletJump = true;
            ammoDisplay.gameObject.SetActive(true);
        }
    }

    public void ApplyUpwardsForce()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, upwardsForce);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    public void ApplyUpwardsForce(float newUpwardsForce)
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, newUpwardsForce);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    // This is just so the new ground check boxCast can be seen in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isEditingGizmos)
        {
            //Gizmos.DrawCube(transform.position - transform.up * maxDistance - yOffset, boxSize); // Code to draw the box cast for the ground check, uncomment if editing it
            Gizmos.DrawCube(transform.position - transform.up * maxDistance - yOffset, jumpableBoxSize); // Code to draw the box cast for the ground check, uncomment if editing it
        }
    }
    
    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position - yOffset, boxSize, 0f, -transform.up, maxDistance, layerMask) || isOnPlatform)
        {            
            // isGrounded = true;
            numOfJumps = maxJumps;
            return true;
        }
        else
        {
            // isGrounded = false;
            return false;
        }
    }

    private bool IsInJumpableArea()
    {
        if(Physics2D.BoxCast(transform.position - yOffset, jumpableBoxSize, 0f, -transform.up, maxDistance, layerMask) || isOnPlatform)
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
        numOfJumps = 0;
        yield return new WaitForSeconds(0.2f);

        yield return null;
    }

    private IEnumerator Attack()
    {
        if (vertical < 0 && isGrounded && absoluteHorizontal < 0)
        {
            //Don't attack if the player is pressing downwards and is on the ground
        }
        else
        {
            isAttacking = true;
            //rigidBody2D.isKinematic = true;
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("Attacking");
            yield return new WaitForSeconds(0.25f); //Waits for the attack animation to finish
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
            //rigidBody2D.isKinematic = false;
        }

        yield return null;
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        /*
        if (dashDirection == Vector2.down)
        {
            if(isGrounded == false)
            {
                StopCoroutine(Dash());
            }
        }
        */
        
        //If there is no directional input the player will face in the direction they were last facing
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(lastMoveHorizontal, 0);
        }

        GameObject dashParticle = Instantiate(bulletJumpParticles, transform.position - new Vector3(horizontal, vertical, 0), Quaternion.identity);
        HandleBulletJumpParticles(dashParticle);

        float originalGravity = rigidBody2D.gravityScale; //Stores original gravity
        float originalVertical = QuantizeAxis(Input.GetAxisRaw("Vertical")); //Stores original vertical direction
        
        rigidBody2D.gravityScale = 0.0f; //Sets player gravity to zero, so they can dash in the air unaffected by gravity
        rigidBody2D.velocity = Vector2.zero; //Resets player velocity, so initial velocity doesn't have any strange interactions with the dash
        yield return new WaitForSeconds(0.01f);
        EndSlideForce();
        numOfDashes -= 1;

        rigidBody2D.velocity = new Vector2(dashDirection.normalized.x * dashStrength, dashDirection.normalized.y * dashStrength); //Dash movement

        yield return new WaitForSeconds(dashTime); //Waits for dash to finish

        rigidBody2D.gravityScale = originalGravity; //Resets gravity to normal
        rigidBody2D.velocity = new Vector2(horizontal * movementSpeed, originalVertical * movementSpeed); //Resets player's velocity
        animator.SetBool("IsDashing", false);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    private void HandleBulletJumpParticles(GameObject dashParticle)
    {
        BulletJumpParticlesController dashParticleScript = dashParticle.GetComponent<BulletJumpParticlesController>();
        dashParticleScript.Initialise(dashDirection);
        
        if (isFacingRight)
        {
            dashParticleScript.FlipSprite();
        }
        
        
        /*
        // Dash direction is (horizontal, vertical)
        switch (dashDirection)
        {
            case Vector2 direction when direction == new Vector2(0, 0): // No Dash Input
                Debug.Log("No input");
                break;
            case Vector2 direction when direction == new Vector2(-1, 1): // Dashing Up Left
                dashParticle.transform.Rotate(0f, 0f, 225f);
                break;
            case Vector2 direction when direction == new Vector2(1, 1): // Dashing Up Right
                dashParticle.transform.Rotate(0f, 0f, -225f);
                break;
            case Vector2 direction when direction == new Vector2(1, -1): // Dashing Down Right
                dashParticle.transform.Rotate(0f, 0f, 45f);
                break;
            case Vector2 direction when direction == new Vector2(-1, -1): // Dashing Down Left
                dashParticle.transform.Rotate(0f, 0f, -45f);
                break;
            case Vector2 direction when direction == new Vector2(-1, 0): // Dashing Left
                dashParticle.transform.Rotate(0f, 0f, -90f);
                break;
            case Vector2 direction when direction == new Vector2(1, 0): // Dashing Right
                dashParticle.transform.Rotate(0f, 0f, 90f);
                break;
            case Vector2 direction when direction == new Vector2(0, 1): // Dashing Up
                dashParticle.transform.Rotate(0f, 0f, 180f);
                break;
            case Vector2 direction when direction == new Vector2(0, -1): // Dashing Down
                dashParticle.transform.Rotate(0f, 0f, 0f);
                break;

            default:
                break;
        }
        */
    
    }
    
    void FixedUpdate()
    {
        //Flips the player's sprite if they move
        if (lastMoveHorizontal > 0 && !isFacingRight && !isSliding)
        {
            FlipSprite();
            ammoDisplay.FlipSprite();
        }
        
        if (lastMoveHorizontal < 0 && isFacingRight && !isSliding)
        {
            FlipSprite();
            ammoDisplay.FlipSprite();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Sliding")
        {
            if (!isDashing)
            {
                //Debug.Log("Sliding");
                isSliding = true;
                hasSlideSpeed = true;
                isTouchingSliding = true;
                notTouchingSlidingTimer = 0f;
                //StopCoroutine(EndSlidingMomentum());
                //StopCoroutine(slideCorotine);
            }
        }

        if(collision.collider.tag == "Terrain")
        {
            Debug.Log("Touched terrain");
            isSliding = false;
            hasSlideSpeed = false;
            isTouchingSliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Sliding")
        {
            if (!isDashing)
            {
                //Debug.Log("Sliding");
                isSliding = true;
                hasSlideSpeed = true;
                isTouchingSliding = true;
                notTouchingSlidingTimer = 0f;
                //StopCoroutine(EndSlidingMomentum());
                //StopCoroutine(slideCorotine);
            }
        }

        if(collider.tag == "Terrain")
        {
            Debug.Log("Touched terrain");
            isSliding = false;
            hasSlideSpeed = false;
            isTouchingSliding = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Sliding")
        {
            if (!isDashing)
            {
                //Debug.Log("Sliding");
                isSliding = true;
                hasSlideSpeed = true;
                isTouchingSliding = true;
                notTouchingSlidingTimer = 0f;
                //StopCoroutine(EndSlidingMomentum());
                //StopCoroutine(slideCorotine);
            }
        }

        if(collider.tag == "Terrain")
        {
            Debug.Log("Touched terrain");
            isSliding = false;
            hasSlideSpeed = false;
            isTouchingSliding = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   
        if(collision.collider.tag == "Sliding")
        {
            if (!isDashing)
            {
                isSliding = true;
                hasSlideSpeed = true;
                isTouchingSliding = true;
                //StopCoroutine(EndSlidingMomentum());
                //StopCoroutine(slideCorotine);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Sliding")
        {
            isTouchingSliding = false;
        }
    }

    private IEnumerator StopSliding()
    {
        yield return new WaitForSeconds(0.05f);

        if (!isTouchingSliding)
        {
            //Debug.Log("Stop sliding");
            isSliding = false;
            //StartCoroutine(EndSlidingMomentum());
            //StartCoroutine(slideCorotine);
        }
    }

    private void EndSlideForce()
    {
        //Debug.Log("End slide called");
        hasSlideSpeed = false;
        isSliding = false;
    }

    private IEnumerator EndSlidingMomentum()
    {
        hasSlideSpeed = false;
        Debug.Log("End slide called");
        yield return new WaitForSeconds(4);
        Debug.Log("End Sliding Momentum");
        //hasSlideSpeed = false;
    }
}
