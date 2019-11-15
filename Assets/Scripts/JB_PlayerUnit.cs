using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class JB_PlayerUnit : NetworkBehaviour
{
    public delegate void WaterLevelAction();
    public static event WaterLevelAction OnWaterButton;

    public HeroType heroType;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D rb;
    private float directionX;

    private bool isGrounded;

    [HideInInspector] public bool canMove = false;    // determines if this player unit is allowed to move
    [HideInInspector] public bool moving = false;     // used to tell if player unit is moving
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
    

    public override void OnStartAuthority()
    {
        if (!hasAuthority) { return; }

        //bLevers.Clear();
        //waterMovable.Clear();
        //waterToggle.Clear();
        

        rb = GetComponent<Rigidbody2D>();
        playerCamera.SetActive(true);
        playerCamera.transform.parent = null;
        userControls.SetActive(true);

        
        // 11 items total in game
        itemsPickedUp = new bool[11];
    }

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
            //waterObjects[i].GetComponent<JB_AdjustWater>().waterToMove = myWaterMovable[i];
            waterObjects[i].GetComponent<JB_AdjustWater>().bToggle = myWaterToggle[i];
        }

        for (int i = 0; i < waterObjects.Length; ++i)
        {
            waterObjects[i].GetComponent<JB_AdjustWater>().waterToMove = myWaterMovable[i];
            //waterObjects[i].GetComponent<JB_AdjustWater>().bToggle = myWaterToggle[i];
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

        if (moving)
        {
            Movement(leftOrRight);
        }
        else
        {
            StopMovement();
        }
        
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

   
    public void OnWaterClick()
    {
        OnWaterButton();

    }
}
