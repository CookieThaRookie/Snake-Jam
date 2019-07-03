using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public GameObject victoryText;
    public GameObject defeatText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "End")
        {
            print("Entered End trigger");
            victoryText.SetActive(true);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            print("Entered Bullet trigger");
            victoryText.SetActive(true);
        }
    }

}
