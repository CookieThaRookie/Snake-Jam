using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Target;

    public float smoothSpeed;

    public Vector3 offset;

    public float rangeInFront = 1; //Value to determine how far ahead of the snake the camera should be

    public float EnemyInRange = 10;
    private GameObject Enemy;
    private GameObject[] Enemies;

    // Update is called once per frame
    void LateUpdate() //Use Fixed instead if too jittered
    {
        Vector3 aheadOf = (Target.transform.up * rangeInFront) + Target.position; //Position in front of snake
        Debug.DrawLine(Target.position, aheadOf, Color.cyan);

        //Vector3 directPosition = Target.position + offset; //No smoothing position

        Vector3 enemiesPosition = EnemyMidpoint(); //Find the midpoint of big groupings
        Debug.DrawLine(transform.position, enemiesPosition, Color.red);

        Vector3 desiredPosition = aheadOf + offset; //The position the camera should move towards
        Debug.DrawLine(transform.position, desiredPosition, Color.blue);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    /// <summary>
    /// Return a Vector3 of all enemies that are within range of the Target
    /// </summary>
    /// <returns></returns>
    Vector3 EnemyMidpoint()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 compPosition = new Vector3();

        foreach (GameObject Enemy in Enemies)
        {
            Vector2 distance = Target.position - Enemy.transform.position;
            if (distance.sqrMagnitude < EnemyInRange * EnemyInRange) //Square the compared for faster calculation
            {
                print(Enemy.name);
                compPosition += Enemy.transform.position;
            }
        }

        Vector3 middle;
        return middle = compPosition / Enemies.Length;
    }
}
