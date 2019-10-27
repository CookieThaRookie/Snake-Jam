using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Set to 0 for endless rooms. NOT IMPLEMENTED")]
    public int roomAmount = 5;

    [Header("NOT IMPLEMENTED")]
    public int phase1End = 0;

    [Header("Special GameObject to hold Tilemaps")]
    public GameObject grid;

    /// <summary>
    /// Just what the name implies.
    /// </summary>
    public GameObject startRoom;
    /// <summary>
    /// Placeholder for the phase system. All rooms between start and finish.
    /// </summary>
    public List<GameObject> listOfRooms;
    /// <summary>
    /// Room at the end of the dungeon. This is a win condition.
    /// </summary>
    public GameObject finishRoom;

    // Start is called before the first frame update
    void Start()
    {
        //Triggers if all required rooms are given in the inspector.
        if(listOfRooms != null && startRoom != null && (finishRoom != null || roomAmount == 0))
        {
            GenerateRooms();
        }
    }


    private void GenerateRooms()
    {
        GameObject currentStartRoom = Instantiate(startRoom, new Vector3(0, 0, 0), Quaternion.identity);
        currentStartRoom.transform.parent = grid.transform;

        GameObject lastRoom = currentStartRoom;
        float MaxY = 0f;

        System.Random rnd = new System.Random();

        int previousRand = rnd.Next(0, listOfRooms.Count);

        for (int i = 0; i < roomAmount; i++)
        {
            Transform[] ts = lastRoom.GetComponentsInChildren<Transform>();
            List<Tilemap> tilemapsOfLastRoom = new List<Tilemap>();
            int tempCount = 0;
            foreach(Transform t in ts)
            {
                if (tempCount != 0)
                {
                    
                    if (t != null && t.gameObject != null)
                    {
                        tilemapsOfLastRoom.Add(t.gameObject.GetComponent<Tilemap>());
                    }
                }

                tempCount++;
            }

            float currentMaxY = 0f;

            foreach(Tilemap tm in tilemapsOfLastRoom)
            {
                if(tm.cellBounds.max.y > currentMaxY)
                {
                    currentMaxY = tm.cellBounds.max.y;
                }
            }

            MaxY += currentMaxY;
            
            GameObject currentRoom = null;

            int currentRand = rnd.Next(0, listOfRooms.Count);

            if(currentRand == previousRand)
            {
                currentRand = rnd.Next(0, listOfRooms.Count);
            }

            if (i == (roomAmount - 1))
            {
                currentRoom = Instantiate(finishRoom, new Vector3(0, MaxY, 0), Quaternion.identity);
            }
            else
            {
                currentRoom = Instantiate(listOfRooms[currentRand], new Vector3(0, MaxY, 0), Quaternion.identity);
            }

            currentRoom.transform.parent = grid.transform;
            lastRoom = currentRoom;
        }
    }
    
}
