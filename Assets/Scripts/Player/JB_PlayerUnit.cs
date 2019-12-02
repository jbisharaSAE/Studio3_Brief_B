using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
//using System;
//using UnityEngine.UI;

public class JB_PlayerUnit : NetworkBehaviour
{
    public delegate void WaterLevelAction();
    public static event WaterLevelAction OnWaterButton;

    public HeroType heroType;

    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    private bool isPC;

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

    [HideInInspector]
    public bool[] itemsPickedUp;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    [Header("Audio")]
    public AudioClip bobJump;
    public AudioClip totJump;
    public AudioClip bobSplash;
    public AudioClip totSplash;
    public AudioClip pickupSound;
    public AudioClip pressureSound;

    private AudioSource audioSource;

    public override void OnStartAuthority()
    {
        if (!hasAuthority) { return; }

        rb = GetComponent<Rigidbody2D>();
        playerCamera.SetActive(true);
        playerCamera.transform.parent = null;
        userControls.SetActive(true);

        
        // 9 items total in game
        //itemsPickedUp = new bool[9];

        // hide host / join buttons
        GameObject go = GameObject.FindGameObjectWithTag("MatchSystem");
        go.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        Debug.Log("ARRAY LENGTH :: " + itemsPickedUp.Length);
    }
    private void Awake()
    {
         itemsPickedUp = new bool[9];

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            isPC = true;
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            isPC = true;
        }
        else
        {
            isPC = false;
        }


    }
    

    #region save_system

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
    #endregion


    #region grocery_list
    private void FindGroceryList(int index, GameObject groceryItem)
    {
        Debug.Log("TESTING FIND GROCERY LIST FUNCTION :: ");
        Debug.Log("INDEX PARAMETER = " + index + " : GROCERY ITEM PARAMETER = " + groceryItem.name);
        

        foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> pair in NetworkServer.objects)
        {
            Debug.Log("Testing for loop");
            if(pair.Value.gameObject.GetComponent<JB_PlayerUnit>())
            {
                Debug.Log("TESTING LOOP IF STATEMENT FOR CMD FUNCTIONS");
                CmdUpdateGroceryList(pair.Value.gameObject, index, groceryItem);
            }
        }
    }

    [Command]
    private void CmdUpdateGroceryList(GameObject playerObj, int index, GameObject groceryItem)
    {
        Debug.Log("TESTING UPDATE GROCERY LIST FUNCTION :: cMD");
        playerObj.GetComponent<JB_PlayerUnit>().itemsPickedUp[index] = true;
        playerObj.GetComponent<JB_GroceryManager>().crossTickObj[index].transform.GetChild(0).gameObject.SetActive(false);  // fist child gameobject is red cross
        playerObj.GetComponent<JB_GroceryManager>().crossTickObj[index].transform.GetChild(1).gameObject.SetActive(true);   // second child gameobject is greentick
        RpcUpdateGroceryList(playerObj, index, groceryItem);
        Destroy(groceryItem);
    }

    [ClientRpc]
    private void RpcUpdateGroceryList(GameObject playerObj, int index, GameObject groceryItem)
    {
        Debug.Log("the INDEX NUMBER PARAMETER = " + index);
        Debug.LogWarning("we reached client call UPDATE LIST");
        playerObj.GetComponent<JB_PlayerUnit>().itemsPickedUp[index] = true;
        playerObj.GetComponent<JB_GroceryManager>().crossTickObj[index].transform.GetChild(0).gameObject.SetActive(false);  // fist child gameobject is red cross
        playerObj.GetComponent<JB_GroceryManager>().crossTickObj[index].transform.GetChild(1).gameObject.SetActive(true);   // second child gameobject is greentick
        Destroy(groceryItem);
    }
    #endregion

    #region movement
    private void FixedUpdate()
    {
        if (!this.hasAuthority) { return; }
        if (!canMove) { return; }

        // checks if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (isPC)
        {
            leftOrRight = (int)Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        Movement(leftOrRight);

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

            // play audio jump
            switch ((int)heroType)
            {
                case 0:
                    //bob
                    audioSource.PlayOneShot(bobJump);
                    break;
                case 1:
                    audioSource.PlayOneShot(totJump);
                    break;
                default:
                    break;
            }
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
            Debug.Log("x velocity = " + rb.velocity.x);
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        // facing left
        else if (rb.velocity.x < -0.9f)
        {
            Debug.Log("x velocity = " + rb.velocity.x);
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
        
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Item")
        {
            int n = col.gameObject.GetComponent<JB_GroceryItem>().numConversion;
            Debug.Log("WE HIT GROCERY ITEM!");
            FindGroceryList(n, col.gameObject);
            audioSource.PlayOneShot(pickupSound);
            Destroy(col.gameObject);
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

                // play audio splash for bob
                audioSource.PlayOneShot(bobSplash);
            }
            else if (heroType == HeroType.Tot)
            {
                Debug.Log("Tot");
                // if tot hits the water, let her have the ability to jump
                isGrounded = true;

                // play audio splash for tot
                audioSource.PlayOneShot(totSplash);
            }

        }
        
    }

    // sending event
    public void OnWaterClick()
    {
        OnWaterButton();

    }

    #region wall_collision
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Vector3 hit = col.contacts[0].normal;

            CollideWallTest(hit);
        }
        else if(col.gameObject.tag == "PressurePlate")
        {
            audioSource.PlayOneShot(pressureSound);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Vector3 hit = col.contacts[0].normal;

            CollideWallTest(hit);
        }

    }
    private void CollideWallTest(Vector3 hit)
    {

        if (hit.x == -1 || hit.y == -1)
        {
            // left direction
            //rb.velocity = Vector2.zero;
            leftOrRight = 0;
        }
        else if (hit.x == 1 || hit.y == -1)
        {
            // right direction
            //rb.velocity = Vector2.zero;
            leftOrRight = 0;
        }
    }
    #endregion

    #region audio_commands

    

    #endregion


}
