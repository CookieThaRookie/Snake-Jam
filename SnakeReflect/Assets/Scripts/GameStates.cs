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

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D wantedCollider = this.GetComponent<CircleCollider2D>();

        if (collision.gameObject.tag == "Bullet" && wantedCollider.IsTouching(collision.collider))
        {
            print("Entered Bullet trigger");
            defeatText.SetActive(true);
        }
    }

}
