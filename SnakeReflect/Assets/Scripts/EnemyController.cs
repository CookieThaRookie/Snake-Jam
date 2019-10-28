using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Target;
    public float moveAtDistance;
    public float movementSpeed;

    public GameObject BulletType;
    public GameObject FirePos;
    public float bulletSpeed;
    public float rateOfFire;
    public float deleteBulletAFter;

    public bool shootingAllowed = true;

    Vector2 direction;
    float angle;

    bool canFire;
    float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = Target.transform.position - transform.position; //Find the direction vector towards Target
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle on vector
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotate to that given angle
        
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
        GameObject Bullet = Instantiate(BulletType, FirePos.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        //Bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
        Destroy(Bullet, deleteBulletAFter);
    }

    void MoveAway()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, -1 * movementSpeed * Time.deltaTime);
    }
}