using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public GameObject bulletPrefab;
    public bool isFacingRight = false;
    public bool isFacingDown = false;
    public float attackTime = 3f;
    public float bulletSpeed = 5f;
    public float bulletLifetime = 5f;
    public float yOffset;
    private float timer;
    private Vector3 direction;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = attackTime;
        direction = Vector3.left;

        if (isFacingRight)
        {
            FlipGameObject();
        }

        if(!isFacingDown)
        {
            yOffset = 0.25f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("Shoot!");
            timer = attackTime;
            animator.SetTrigger("Firing");
            SpawnProjectile();
        }

        animator.SetBool("IsFacingDown", isFacingDown);

        if (isFacingDown)
        {
            direction = Vector3.down;
        }
        else if(isFacingRight)
        {
            direction = Vector3.right;
        }
        else if(!isFacingRight)
        {
            direction = Vector3.left;
        }
    }

    private void SpawnProjectile()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position - new Vector3(0, yOffset, 0), Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Initialise(direction, bulletSpeed, bulletLifetime, isFacingDown, isFacingRight);
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
