using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class JB_PlayerUnit : NetworkBehaviour
{
    public HeroType heroType;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D rb;
    private float directionX;
    private bool isGrounded;

    public GameObject playerCamera;
    public bool canMove = false;

    public override void OnStartAuthority()
    {
        if (!hasAuthority) { return; }

        rb = GetComponent<Rigidbody2D>();
        playerCamera.SetActive(true);
        playerCamera.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.hasAuthority) { return; }
        if (!canMove) { return; }

        directionX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!this.hasAuthority) { return; }
        if (!canMove) { return; }

        rb.velocity = new Vector2(directionX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered");
        if(collision.gameObject.tag == "LeverTrigger")
        {
            collision.gameObject.GetComponent<JB_LeverTrigger>().bToggle = !collision.gameObject.GetComponent<JB_LeverTrigger>().bToggle;
            Debug.Log("player hit lever trigger");
        }
    }
}
