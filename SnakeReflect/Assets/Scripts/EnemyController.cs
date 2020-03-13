using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveAtDistance;
    public float movementSpeed;

    public GameObject BulletType;
    
    public float bulletSpeed;
    public float rateOfFire;
    public float deleteBulletAFter;

    public bool shootingAllowed = true;

    private Vector2 direction;
    private float angle;
    private bool canFire;
    private float nextFire = 0;
    private GameObject Target;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Target.transform.position - transform.position; //Find the direction vector towards Target

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle on vector

        // Flips the Sprite towards the player's direction
        if (direction.x > 1)
        {
            spriteRenderer.flipX = false;
        }

        else
        {
            spriteRenderer.flipX = true;
        }
        

        // Backs away if player gets close
        if (direction.magnitude < moveAtDistance)
        {
            if (direction.magnitude < 1)
                return;

            print("Moving!");
            MoveAway();
        }

        //Shooting intervals
        if (shootingAllowed && Time.time > nextFire)
        {
            Shoot();
            nextFire = Time.time + rateOfFire;
        }
    }

    void Shoot()
    {
        print("Pew");
        GameObject Bullet = Instantiate(BulletType, this.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        //Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
        Destroy(Bullet, deleteBulletAFter);
    }

    void MoveAway()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, -1 * movementSpeed * Time.deltaTime);
    }
}