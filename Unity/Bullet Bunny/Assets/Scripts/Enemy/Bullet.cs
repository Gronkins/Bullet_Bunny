using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerStats playerStats;
    //new Rigidbody2D rigidbody2D;
    private float bulletSpeed;
    private float bulletLifetime;
    private Vector3 direction;
    private bool isFacingRight = false;
    private bool isFacingDown = false;
    private Animator animator;
    private bool hasCollided = false;
    private bool hasBeenDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenDestroyed = false;
        playerStats = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();

        StartCoroutine(DestroyBullet());
        //rigidbody2D = GetComponent<Rigidbody2D>();
        
        
        if (isFacingDown)
        {
            //transform.Rotate(0f, 0f, 90f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
            transform.rotation *= rotation;
        }

        if (isFacingRight && !isFacingDown)
        {
            FlipGameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody2D.velocity = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
        if (!hasCollided)
        {
            transform.position += direction * bulletSpeed * Time.deltaTime;
        }
    }

    public void Initialise(Vector3 targetPosition, float speed, float lifetime, bool isFacingDownwards, bool isFacingRightSide)
    {
        bulletSpeed = speed;
        direction = targetPosition;
        bulletLifetime = lifetime;
        isFacingRight = isFacingRightSide;
        isFacingDown = isFacingDownwards;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerHurtbox" || collision.collider.tag == "Player")
        {
            //Debug.Log("Bullet hit player");
            if (!hasBeenDestroyed)
            {
                hasBeenDestroyed = true;
                playerStats.TakeDamage();
                HandleBulletDestruction();
            }
        }

        Debug.Log("Collided with something (Bullet)");

        if (collision.collider.tag == "Terrain")
        {
            //Debug.Log("Collided with terrain");
            HandleBulletDestruction();
        }
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerHurtbox" || collider.tag == "Player")
        {
            if (!hasBeenDestroyed)
            {
                //Debug.Log("Bullet hit player");
                hasBeenDestroyed = true;
                playerStats.TakeDamage();
                HandleBulletDestruction();
                //Destroy(gameObject);
            }
        }

        Debug.Log("Collided with something (Bullet)");

        if (collider.tag == "Terrain")
        {
            //Debug.Log("Collided with terrain");
            HandleBulletDestruction();
            //Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(gameObject);
    }

    private void HandleBulletDestruction()
    {
        hasCollided = true;
        animator.SetTrigger("Destroy");
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void FlipGameObject()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1; //Inverses the player's game object
        gameObject.transform.localScale = currentScale;

        isFacingRight = true;
        direction = Vector3.right;
    }
}
