using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class JB_LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public JB_PlayerUnit playerUnitScript;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("button held");
        playerUnitScript.leftOrRight = -1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("button released");
        playerUnitScript.leftOrRight = 0;
        //playerUnitScript.moving = false;
        
    }

   
}
