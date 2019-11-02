using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(new Vector2(0,speed));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(new Vector2(0, -speed));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(new Vector2(speed, 0));
        }
    }
}
