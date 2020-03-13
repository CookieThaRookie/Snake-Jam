using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Object to focus on
    public float speed; // How fast should the camera move?
    public float distance = 1; // How far ahead the camera should look

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.deltaTime); // Smoothly move camera towards target
        transform.position += (target.up * distance) + Vector3.back * 10; 
    }
}
