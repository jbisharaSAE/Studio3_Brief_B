using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// This script is being used to track the amount of time a player spends inside of a puzzle.
/// </summary>
public class CS_AnalyticsTimer : MonoBehaviour
{

    public float timer = 0f;
    public bool countingUp = false;


    // Update is called once per frame
    void Update()
    {
        if(countingUp)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherPlayer)
    {
        if(otherPlayer.TryGetComponent(out JB_PlayerUnit player))
        {
            timer = 0f;
            countingUp = true;
            Debug.Log("Enter trigger");

 
        }
    }

    private void OnTriggerExit2D(Collider2D otherPlayer)
    {
        if (otherPlayer.TryGetComponent(out JB_PlayerUnit player))
        {
            countingUp = false;
            string newMessage = "";

            //                  "The player left puzzle x. It took them y seconds to complete it.";
            // [JB_PlayerUnit_Tot(Clone)] left [Analytics Puzzle Time-tracker - Tutorial], taking [0.1234567] seconds."
            // TESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTING
            if (otherPlayer.gameObject.name.Contains("Bob"))
            {
                newMessage = "Bob left [" + gameObject.name + " - [" + timer + "] sec.";
            }
            else if (otherPlayer.gameObject.name.Contains("Tot"))
            {
                newMessage = "Tot left [" + gameObject.name + " - [" + timer + "] sec.";
            }

            Analytics.CustomEvent(newMessage);

            Debug.Log(newMessage);
        }
    }
}
