using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class JB_NetworkManager : NetworkManager
{
    
    public void StartHosting()
    {
        StartMatchMaker();
        matchMaker.CreateMatch("Bob'n'Tot", 2, true, "", "", "", 0, 0, OnMatchCreated);
        //RefreshMatches();
        
        
    }

    private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
    {
        base.StartHost(responseData);
        
    }

    

    public void RefreshMatches()
    {
        

        if(matchMaker == null)
        {
            StartMatchMaker();
        }

        matchMaker.ListMatches(0, 10, "", true, 0, 0, HandleListMatchesComplete);
    }

    public void JoinMatch(MatchInfoSnapshot match)
    {
        if(matchMaker == null)
        {
            StartMatchMaker();
        }

        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedInfo, MatchInfo responseData)
    {
        StartClient(responseData);
    }

    private void HandleListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        JB_AvailableMatchesList.HandleNewMatchList(responseData);
    }
}

