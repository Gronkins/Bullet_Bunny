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
    private bool isFacingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        StartCoroutine(DestroyBullet());
        //rigidbody2D = GetComponent<Rigidbody2D>();
        if (isFacingDown)
        {
            //transform.Rotate(0f, 0f, 90f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
            transform.rotation *= rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody2D.velocity = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void Initialise(Vector3 targetPosition, float speed, float lifetime, bool isFacingDownwards)
    {
        bulletSpeed = speed;
        direction = targetPosition;
        bulletLifetime = lifetime;
        isFacingDown = isFacingDownwards;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerHurtbox")
        {
            Debug.Log("Enemy hit player");
            playerStats.playerHealth -= 1;
            Destroy(gameObject);
        }

        /*
        if (collision.collider.tag == "PlayerWeapon")
        {
            Debug.Log("Is hit with player Weapon");
            Destroy(gameObject);
        }
        */
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(gameObject);
    }

    /*
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with something");
        if (collider.tag == "PlayerWeapon")
        {
            Debug.Log("Player hit enemy with weapon");
            Destroy(gameObject);
        }
    }
    */
}
