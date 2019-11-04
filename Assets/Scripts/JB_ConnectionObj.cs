using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum HeroType { Bob = 0, Tot = 1 }

public class JB_ConnectionObj : NetworkBehaviour
{
    // buttons for selecting bob or tot
    public GameObject selectionPhaseObj;

    // player's object to instantiate, bob or tot
    public GameObject playerBobPrefab;
    public GameObject playerTotPrefab;

    // buttons for hero selection, to be disabled when player makes selection
    public Button bobButton;
    public Button totButton;

    [SyncVar]
    public bool isReady;

    // player connection objects
    private GameObject[] connectionObjects;

    private HeroType heroType;

    private Camera mainCam;
    private Transform mainCamWp;

    private GameObject playerUnit;

    // length of 2, one for each player
    private List<bool> ready = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        if (!this.isLocalPlayer) { return; }

        // begin selection phase
        selectionPhaseObj.SetActive(true);

        mainCam = Camera.main;

        mainCamWp = GameObject.FindGameObjectWithTag("CameraWP").GetComponent<Transform>();

    }

    // called when player clicks on button
    public void SpawnCharacter(int hType)
    {
        if (!this.isLocalPlayer) { return; }
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

        mainCam.transform.position = mainCamWp.position;

        CheckPlayerReady();
    }

    private void CheckPlayerReady()
    {
        // find all the player connection objects in scene
        connectionObjects = GameObject.FindGameObjectsWithTag(this.tag);

        ready.Clear();

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
            Debug.Log("game started!");
        }
        else
        {
            selectionPhaseObj.SetActive(false);
            // wait for other player - TODO
        }
   
    }

    [Command]
    public void CmdSpawnCharacter(HeroType hType)
    {
        isReady = true;

        // find all the player connection objects in scene
        connectionObjects = GameObject.FindGameObjectsWithTag(this.tag);

        // disabling the hero button that got selected - TODO does not work on other client

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
        Vector2 spawnPos = new Vector2(12f, 0f);

        switch (hType)
        {
            case HeroType.Bob:
                playerUnit = Instantiate(playerBobPrefab, spawnPos, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(playerUnit, connectionToClient);
                RpcSpawnCharacter(playerUnit);
                break;
            case HeroType.Tot:
                playerUnit = Instantiate(playerTotPrefab, spawnPos, Quaternion.identity);
                NetworkServer.SpawnWithClientAuthority(playerUnit, connectionToClient);
                RpcSpawnCharacter(playerUnit);
                break;
            default:
                Debug.Log("default case reached");
                break;

        }


        //RpcSpawnCharacter(go, hType);

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
