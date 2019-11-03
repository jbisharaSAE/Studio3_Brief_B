using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_PressurePlate : MonoBehaviour
{
    
    public float scaleSpeed;

    public bool reset = false;

    private Vector2 startPos;
    private Vector2 endPos;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(transform.position.x, transform.position.y - 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (!reset)
        {
            // move button down, using layers to hide the button
            transform.position = Vector2.MoveTowards(startPos, endPos, step);
            
        }
        else 
        {
            // move button down, using layers to hide the button
            transform.position = Vector2.MoveTowards(endPos, startPos, step);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        reset = false;
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        if(collision.gameObject.GetComponent<JB_PlayerUnit>().heroType == HeroType.Bob)
    //        {
    //            float xScale = 0f; // width scale
    //            float yScale = 0.08f; // starting size of pressure plate
    //            float zScale = 0f;


    //            if (yScale > 0.01f)
    //            {
    //                yScale -= scaleSpeed;
    //                Vector3 scaleSize = new Vector3(xScale, yScale, zScale);
    //                transform.localScale -= scaleSize;
    //            }

                
    //        }
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        reset = true;
    }
}
