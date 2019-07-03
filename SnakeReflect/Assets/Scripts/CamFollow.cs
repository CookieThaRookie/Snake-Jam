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
    private int enemiesCount = 0;

    // Update is called once per frame
    void LateUpdate() //Use Fixed instead if too jittered
    {
        Vector3 aheadOf = (Target.transform.up * rangeInFront) + Target.position; //Position in front of snake
        Debug.DrawLine(Target.position, aheadOf, Color.cyan);

        //Vector3 directPosition = Target.position + offset; //No smoothing position

        Vector3 enemiesMidpoint = EnemyMidpoint();
        Vector3 enemiesPosition = enemiesMidpoint - aheadOf; //Find the midpoint of big groupings
        Debug.DrawLine(aheadOf, enemiesMidpoint, Color.red);

        Vector3 desiredPosition = new Vector3();

        if (enemiesMidpoint != Vector3.zero)
        {
            float eCount = enemiesCount;
            float enemyCountWeight = 1- 0.5f * (1 / eCount);
            enemiesPosition = enemiesPosition * enemyCountWeight;
            desiredPosition = aheadOf + offset + enemiesPosition; //The position the camera should move towards
        }
        else
        {
            desiredPosition = aheadOf + offset;
        }
        
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

        float totalX = 0;
        float totalY = 0;

        enemiesCount = 0;

        foreach (GameObject Enemy in Enemies)
        {
            Vector2 distance = Target.position - Enemy.transform.position;
            if (distance.sqrMagnitude < EnemyInRange * EnemyInRange) //Square the compared for faster calculation
            {
                totalX += Enemy.transform.position.x;
                totalY += Enemy.transform.position.y;
                enemiesCount++;
            }
        }

        float midX = totalX / enemiesCount;
        float midY = totalY / enemiesCount;
        Vector3 compPosition = new Vector3(midX, midY, 0);

        Vector3 middle;
        if (enemiesCount > 0)
            return middle = compPosition;
        else
            return Vector3.zero;
    }
}
