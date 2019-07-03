using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRicochet : MonoBehaviour
{
    public float speed;
    public float distanceToHit;
    public LayerMask collisionMask;

    public int numberOfBounces = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        Ray2D ray = new Ray2D(transform.position, transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceToHit, collisionMask);
        if (hit.collider != null)
        {
            
            Debug.DrawLine(ray.origin, hit.point);
            Vector2 vectorReflect = Vector2.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(vectorReflect.x, vectorReflect.y) * Mathf.Rad2Deg;
            Debug.DrawRay(hit.point, vectorReflect, Color.red);
            transform.eulerAngles = new Vector3(0, 0, rot);

            numberOfBounces--;
        }

        if (numberOfBounces == 0)
        {
            Destroy(this.gameObject, 0);
        }
    }
}
