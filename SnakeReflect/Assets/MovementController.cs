using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 3f;
    public float turnSpeed = 200f;

    float horizontal = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, // FixedDeltaTime will even out the movement on different update times
            Space.Self); // Translate next based on own position
        transform.Rotate(Vector3.forward * -horizontal * turnSpeed * Time.fixedDeltaTime);
    }
}
