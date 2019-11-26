using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using UnityEngine.UI;

public class JB_PlayerUnit : NetworkBehaviour
{
    public delegate void WaterLevelAction();
    public static event WaterLevelAction OnWaterButton;

    public HeroType heroType;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D rb;
    private float groundRadius = 0.15f;
    public bool isGrounded;

    [HideInInspector] public bool canMove = false;    // determines if this player unit is allowed to move
    [HideInInspector] public int leftOrRight;         // int used to determine which direction the player is moving

    public GameObject playerCamera;
    public GameObject userControls;

    public GameObject activateButton;
    public GameObject blackTextBoxArea;
    public TextMeshProUGUI dialogueTextBox;

    // data to save for scene
    private GameObject[] leverObjects;
    private GameObject[] waterObjects;

    [HideInInspector] public List<bool> bLevers = new List<bool>();

    [HideInInspector] public List<bool> waterMovable = new List<bool>();
    [HideInInspector] public List<bool> waterToggle = new List<bool>();

    public bool[] itemsPickedUp;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    public override void OnStartAuthority()
    {
        if (!hasAuthority) { return; }

        rb = GetComponent<Rigidbody2D>();
        playerCamera.SetActive(true);
        playerCamera.transform.parent = null;
        userControls.SetActive(true);

        
        // 9 items total in game
        itemsPickedUp = new bool[JB_GroceryManager.numberOfItems];

        // hide host / join buttons
        GameObject go = GameObject.FindGameObjectWithTag("MatchSystem");
        go.SetActive(false);

        // find grocery button
        GameObject listButton = GameObject.FindGameObjectWithTag("ListButton");
        listButton.GetComponent<Image>().enabled = true;
        listButton.GetComponent<Button>().enabled = true;
        listButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    // used when saving game
    public void FindSceneItems()
    {
        leverObjects = GameObject.FindGameObjectsWithTag("LeverTrigger");
        waterObjects = GameObject.FindGameObjectsWithTag("WaterTrigger");

        foreach (GameObject leverObj in leverObjects)
        {
            bLevers.Add(leverObj.GetComponent<JB_LeverTrigger>().bToggle);
        }

        foreach (GameObject waterObj in waterObjects)
        {
            
            waterToggle.Add(waterObj.GetComponent<JB_AdjustWater>().bToggle);
        }

        foreach(GameObject waterObj in waterObjects)
        {
            waterMovable.Add(waterObj.GetComponent<JB_AdjustWater>().waterToMove);
        }

    }

    // used when loading game
    public void LoadSceneItems(List<bool> myLevers, List<bool> myWaterMovable, List<bool> myWaterToggle)
    {
        leverObjects = GameObject.FindGameObjectsWithTag("LeverTrigger");
        waterObjects = GameObject.FindGameObjectsWithTag("WaterTrigger");

        for(int i = 0; i < leverObjects.Length; ++i)
        {
            leverObjects[i].GetComponent<JB_LeverTrigger>().bToggle = myLevers[i];
        }

        for (int i = 0; i < waterObjects.Length; ++i)
        {
            
            waterObjects[i].GetComponent<JB_AdjustWater>().bToggle = myWaterToggle[i];
        }

        for (int i = 0; i < waterObjects.Length; ++i)
        {
            waterObjects[i].GetComponent<JB_AdjustWater>().waterToMove = myWaterMovable[i];
            
        }
    }

    public void AddItem(int index)
    {
        itemsPickedUp[index] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.hasAuthority) { return; }
        if (!canMove) { return; }

        //directionX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!this.hasAuthority) { return; }
        if (!canMove) { return; }

        //rb.velocity = new Vector2(directionX, rb.velocity.y);

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    Jump();
        //}

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        Movement(leftOrRight);

        //else
        //{
        //    StopMovement();
        //}

    }

    private void StopMovement()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

    }

    public void Jump()
    {
        Debug.LogWarning("Jump activated!");
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        
    }

    public void Movement(int leftRight)
    {
        if (!canMove) { return; }

        //Debug.Log("testing press fuction");

        float direction = leftRight * moveSpeed;

        rb.velocity = new Vector2(direction, rb.velocity.y);
        
        // facing right
        if(rb.velocity.x > 0.9f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        // facing left
        else if (rb.velocity.x < -0.9f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.LogWarning("hit the water!");
            if (heroType == HeroType.Bob)
            {
                // if bob hits the water, disable collider
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
            }
            else if (heroType == HeroType.Tot)
            {
                Debug.Log("Tot");
                // if tot hits the water, let her have the ability to jump
                isGrounded = true;
            }

        }
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

   
    public void OnWaterClick()
    {
        OnWaterButton();

    }
}
