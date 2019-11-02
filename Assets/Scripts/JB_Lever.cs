using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Lever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 9.8f gravity to the right ... -9.8f gravity to left
        //Physics2D.gravity = new Vector2(9.8f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit wall / lever trigger");
        }
    }
}
