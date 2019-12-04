using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public enum HeroType { Bob = 0, Tot = 1 }

public class JB_ConnectionObj : NetworkBehaviour
{
    // buttons for selecting bob or tot
    public GameObject selectionPhaseObj;

    // player's object to instantiate, bob or tot
    public GameObject playerBobPrefab;
    public GameObject playerTotPrefab;

    // spawn point for player units
    private Transform playerSpawnPoint;

    // buttons for hero selection, to be disabled when player makes selection
    public Button bobButton;
    public Button totButton;

    [SyncVar]
    public bool isReady;

    [SyncVar]
    public int playerConnections;

    public GameObject waitingForPlayerTextBox;

    [Header("Grocery item prefabs")]
    [SerializeField]
    private GameObject[] groceryItemsPrefabs;
    private GameObject[] groceryItems;
    private bool runOnce = false;

    // player connection objects
    private GameObject[] connectionObjects;

    private HeroType heroType;

    private Camera mainCam;
    private Transform mainCamWp;

    private GameObject playerUnit;
    private GameObject[] itemSpawnPoints;
    
    // length of 2, one for each player
    private List<bool> ready = new List<bool>();

    public override void OnStartServer()
    {
        groceryItems = new GameObject[9];

        Debug.Log("start server called");
        GameObject[] objects = GameObject.FindGameObjectsWithTag(this.tag);

        foreach(GameObject obj in objects)
        {
            Debug.Log("started for loop");
            obj.GetComponent<JB_ConnectionObj>().playerConnections = NetworkServer.connections.Count;
        }

      

    }

    private void CmdSpawnGroceries()
    {
        for(int i = 0; i<groceryItemsPrefabs.Length; ++i)
        {
            groceryItems[i] = Instantiate(groceryItemsPrefabs[i], itemSpawnPoints[i].transform.position, Quaternion.identity);
            if (NetworkServer.FindLocalObject(groceryItems[i].GetComponent<NetworkIdentity>().netId) == null)
            {
                NetworkServer.Spawn(groceryItems[i]);
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.Find("JB_PlayerUnitSpawnPoint").GetComponent<Transform>();

        if (!this.isLocalPlayer) { return; }

        // begin selection phase
        selectionPhaseObj.SetActive(true);

        mainCam = Camera.main;

        //mainCamWp = GameObject.FindGameObjectWithTag("CameraWP").GetComponent<Transform>();

    }

    // called when player clicks on button
    public void SpawnCharacter(int hType)
    {
        if (!this.isLocalPlayer) { return; }

        if (isServer && !runOnce)
        {
            itemSpawnPoints = GameObject.FindGameObjectsWithTag("GrocerySpawn").OrderBy(go => go.name).ToArray();

            CmdSpawnGroceries();
            runOnce = true;
        }

        if (playerConnections > 1) // checking to see if both players have joined
        {
            // 0 for bob, 1 for tot ... button OnClick() does not take enum for parameters
            switch (hType)
            {
                case 0:
                    heroType = HeroType.Bob;
                    break;
                case 1:
                    heroType = HeroType.Tot;
                    break;
                default:
                    break;
            }

            CmdSpawnCharacter(heroType);


            CheckPlayerReady();
        }
        
    }

   
    private void CheckPlayerReady()
    {
        // find all the player connection objects in scene
        connectionObjects = GameObject.FindGameObjectsWithTag(this.tag);

        //ready.Clear();

        foreach(KeyValuePair<NetworkInstanceId,NetworkIdentity> pair in NetworkServer.objects)
        {
            if(pair.Value.gameObject.tag == "PlayerConnection")
            {
                ready.Add(pair.Value.gameObject.GetComponent<JB_ConnectionObj>().isReady);
            }
            
        }

        // checks to see if all booleans in ready are true (from each player)
        bool all = ready.All(x => x);

        if (all)
        {
            // start game - TODO
            selectionPhaseObj.SetActive(false);
            
            CmdEnableController();
            Debug.Log("game started!");
        }
        else
        {
            selectionPhaseObj.SetActive(false);
            waitingForPlayerTextBox.SetActive(true);
            Debug.Log("one player ready");
            // wait for other player - TODO
        }
   
    }


    [Command]
    void CmdEnableController()
    {
        foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> pair in NetworkServer.objects)
        {
            if (pair.Value.gameObject.tag == "Player")
            {
                pair.Value.gameObject.GetComponent<JB_PlayerUnit>().canMove = true;
                
                RpcEnableController(pair.Value.gameObject);
            }

            if(pair.Value.gameObject.tag == "PlayerConnection")
            {
                pair.Value.gameObject.GetComponent<JB_ConnectionObj>().waitingForPlayerTextBox.SetActive(false);
                RpcTurnOffWaitMessage(pair.Value.gameObject);
            }
        }

        
    }

    [ClientRpc]
    void RpcTurnOffWaitMessage(GameObject obj)
    {
        obj.GetComponent<JB_ConnectionObj>().waitingForPlayerTextBox.SetActive(false);
    }

    [ClientRpc]
    void RpcEnableController(GameObject playerObj)
    {
    
        playerObj.GetComponent<JB_PlayerUnit>().canMove = true;
    }

    [Command]
    public void CmdSpawnCharacter(HeroType hType)
    {
        
        isReady = true;

        // find all the player connection objects in scene
        connectionObjects = GameObject.FindGameObjectsWithTag(this.tag);

        // disabling the hero button that got selected 

        foreach(GameObject obj in connectionObjects)
        {
            // bob button
            if (hType == HeroType.Bob)
            {
                CmdDisableButton(obj, 0);
            }
            // tot button
            else
            {
                selectionPhaseObj.SetActive(false);
                CmdDisableButton(obj, 1);
            }
        }

        

        // 12 units to the right of starting spawn
        //Vector2 spawnPos = new Vector2(31f, -0.1f);

        // spawn correct unit type based off player button click from main menu
        switch (hType)
        {
            case HeroType.Bob:
                playerUnit = Instantiate(playerBobPrefab, playerSpawnPoint.position, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(playerUnit, connectionToClient);
                RpcSpawnCharacter(playerUnit);
                break;
            case HeroType.Tot:
                playerUnit = Instantiate(playerTotPrefab, playerSpawnPoint.position, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(playerUnit, connectionToClient);
                RpcSpawnCharacter(playerUnit);
                break;
            default:
                Debug.Log("default case reached");
                break;

        }

    }

    [ClientRpc]
    public void RpcSpawnCharacter(GameObject unitObj)
    {
        playerUnit = unitObj;

    }

    [Command]
    void CmdDisableButton(GameObject playerObj, int enumIndex)
    {
        // 0 for bob button, 1 for tot button
        // disables the button so the other player cannot select the same character
        switch (enumIndex)
        {
            case 0:
                playerObj.GetComponent<JB_ConnectionObj>().bobButton.interactable = false;
                break;
            case 1:
                playerObj.GetComponent<JB_ConnectionObj>().totButton.interactable = false;
                break;
            default:
                break;
        }

        

        RpcDisableButton(playerObj, enumIndex);
    }

    [ClientRpc]
    void RpcDisableButton(GameObject playerObj, int enumIndex)
    {
        // 0 for bob button, 1 for tot button
        // disables the button so the other player cannot select the same character
        switch (enumIndex)
        {
            case 0:
                playerObj.GetComponent<JB_ConnectionObj>().bobButton.interactable = false;
                break;
            case 1:
                playerObj.GetComponent<JB_ConnectionObj>().totButton.interactable = false;
                break;
            default:
                break;
        }
    }
    
}
