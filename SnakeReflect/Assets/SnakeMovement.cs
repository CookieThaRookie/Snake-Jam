using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

    public float minDistance = 0.25f;
    public int beginSize;
    public float speed = 1;
    public float rotationSpeed = 50;

    public GameObject BodyPrefab;

    private float distance;
    private Transform curBodyPart;
    private Transform prevBodyPart;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < beginSize - 1; i++)
        {
            AddBodyPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Test add body part
        if (Input.GetKeyDown(KeyCode.Space))
            AddBodyPart();
    }

    public void Move()
    {
        float curSpeed = speed;

        if (Input.GetKey(KeyCode.W)) //Moves twice as fast while pressing W
            curSpeed *= 2;

        BodyParts[0].Translate(BodyParts[0].up * curSpeed * Time.smoothDeltaTime, Space.World); //Moves the head of the snake forward

        if (Input.GetAxis("Horizontal") != 0)
            BodyParts[0].Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal")); //Vector 3 forward is UP in 2D. Rotate around this

        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            distance = Vector3.Distance(prevBodyPart.position, curBodyPart.position);
            Vector3 newPos = prevBodyPart.position;
            newPos.z = BodyParts[0].position.z;
            float T = Time.deltaTime * distance / minDistance * curSpeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform newPart = (Instantiate(BodyPrefab, 
            BodyParts[BodyParts.Count - 1].position, 
            BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.SetParent(transform);
        BodyParts.Add(newPart);
    }
}
