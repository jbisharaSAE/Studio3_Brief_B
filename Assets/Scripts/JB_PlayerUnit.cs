using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class JB_PlayerUnit : NetworkBehaviour
{
    public delegate void WaterLevelAction();
    public static event WaterLevelAction OnWaterButton;

    public HeroType heroType;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D rb;
    private float directionX;

    public bool isGrounded;
    public bool canMove = false;

    public GameObject playerCamera;

    public GameObject activateButton;
    public GameObject blackTextBoxArea;
    public TextMeshProUGUI dialogueTextBox;
    

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

        //rb.velocity = new Vector2(directionX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        
    }

    public void Movement(int leftRight)
    {
        if (!canMove) { return; }

        Debug.Log("testing press fuction");

        float direction = leftRight * moveSpeed;

        rb.velocity = new Vector2(direction, rb.velocity.y);
    }

    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    

   public void OnWaterClick()
    {
        OnWaterButton();

    }
}
