using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;

public class JB_JoinButton : NetworkBehaviour
{
    private TextMeshProUGUI buttonText;
    private MatchInfoSnapshot match;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Button>().onClick.AddListener(JoinMatch);
    }

    public void Initialise(MatchInfoSnapshot match, Transform panelTransform)
    {
        this.match = match;
        buttonText.text = match.name;

        if (!isServer)
        {
            transform.SetParent(panelTransform);
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
        }
        
    }

    public void JoinMatch()
    {
        FindObjectOfType<JB_NetworkManager>().JoinMatch(match);
    }
}
