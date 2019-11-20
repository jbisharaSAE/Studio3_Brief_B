using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;


public class JB_MatchListPanel : MonoBehaviour
{
    [SerializeField]
    private JB_JoinButton joinButtonPrefab;

    private void Awake()
    {
        JB_AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        ClearExistingButtons();
        CreateNewJoinGameButtons(matches);
    }

    
    private void ClearExistingButtons()
    {
        var buttons = GetComponentsInChildren<JB_JoinButton>();
        
        foreach(var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }

    private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches)
    {

        foreach (var match in matches)
        {
            var button = Instantiate(joinButtonPrefab);
            button.Initialise(match, transform);
        }
    }

    
}
