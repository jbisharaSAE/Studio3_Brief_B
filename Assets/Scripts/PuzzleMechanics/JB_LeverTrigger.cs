using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_LeverTrigger : MonoBehaviour
{
    public Transform lever;
    public Transform platform;

    public bool bToggle;

    public float platformSpeed = 0.1f;
    public float rotateSpeed = 200f;

    public Transform leftTarget;
    public Transform rightTarget;

    public Transform locationOne;
    public Transform locationTwo;

    private Rigidbody2D leverRB;
    

    private void Awake()
    {
        leverRB = lever.GetComponent<Rigidbody2D>();

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        

        // this boolean toggles when ever player enters trigger, alternates between rotating between left and right
        // all levers will start from the left side rotation
        if (bToggle)
        {
            // if it's on left side, rotate lever  to -35
            RotateLever(rightTarget.position);

        }
        else 
        {
            // if it's on right side, rotate lever  to 35
            RotateLever(leftTarget.position);

        }

        
    }

    private void RotateLever(Vector3 targetPos)
    {
        Vector2 direction = (Vector2)targetPos - leverRB.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, lever.transform.up).z;

        leverRB.angularVelocity = -rotateAmount * rotateSpeed;
    }

    private void Update()
    {
        if (bToggle)
        {
            MovePlatform(locationTwo.position);
        }
        else
        {
            MovePlatform(locationOne.position);
        }
    }

    private void MovePlatform(Vector3 targetPos)
    {
        float step = platformSpeed * Time.deltaTime;

        //Vector2 direction = platform.position - targetPos;
        //float distance = Vector2.Distance(platform.position, targetPos);

        platform.position = Vector2.MoveTowards(platform.position, targetPos, step);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bToggle = !bToggle;
        }
    }
}
