using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_LeverTrigger : MonoBehaviour
{
    public Transform lever;

    public bool bToggle;

    public float speed = 0.1f;

    public Transform leftTarget;
    public Transform rightTarget;

    private Rigidbody2D leverRB;

    private bool isRotating;
    public float rotateSpeed = 200f;

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

}
