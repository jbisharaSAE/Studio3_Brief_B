using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_PressurePlate : MonoBehaviour
{
    
    public bool reset = false;
    public float speed = 2f;

    public Transform platform;
    public Transform platformStartPos;
    public Transform platformEndPos;

    private float yPos;
    private float minClamp;

    private Rigidbody2D rb;
    private bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (pressed)
        {
            platform.position = Vector2.MoveTowards(platform.position, platformEndPos.position, step);
        }
        else
        {
            platform.position = Vector2.MoveTowards(platform.position, platformStartPos.position, step);
        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<JB_PlayerUnit>().heroType == HeroType.Bob)
            {
                pressed = true;
                Debug.Log("testing bob");
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        pressed = false;
    }
}
