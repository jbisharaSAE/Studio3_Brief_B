using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;

public class JB_NavigationPointer : MonoBehaviour
{
    
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera cam;
    [SerializeField] HeroType hType;
    private bool isOffScreen;
    private Image graphic;

    private void Awake()
    {
        //targetPosition = new Vector3(200, 45);
        pointerRectTransform = transform.Find("Graphic").GetComponent<RectTransform>();
        graphic = GetComponentInChildren<Image>();
        
    }

    private void Start()
    {
        // we need to set the targetposition of the other player 
        switch (hType)
        {
            case HeroType.Bob:
                FindOtherPlayer(HeroType.Tot);
                break;
            case HeroType.Tot:
                FindOtherPlayer(HeroType.Bob);
                break;
            default:
                break;
        }
    }

    private void FindOtherPlayer(HeroType hType)
    {
        GameObject[] otherPlayer = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in otherPlayer)
        {
            if(player.GetComponent<JB_PlayerUnit>().heroType == hType)
            {
                targetPosition = player.transform.position;
                Debug.Log(targetPosition);
            }
        }
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = cam.transform.position;

        fromPosition.z = 0f;

        Vector3 dir = (toPosition - fromPosition);

        dir.Normalize();

        float angle = UtilsClass.GetAngleFromVector(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 200f;
        Vector3 targetPosScreenPoint = cam.WorldToScreenPoint(targetPosition);

        if(targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x >= Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize){
            isOffScreen = true;
        }
        else
        {
            isOffScreen = false;
        }

        Debug.Log("Is Off Screen = " + isOffScreen);

        if (isOffScreen)
        {
            graphic.enabled = true;
            Vector3 cappedTargetScreenPosition = targetPosScreenPoint;

            if (cappedTargetScreenPosition.x <= 0) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.height) cappedTargetScreenPosition.x = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        }
        else
        {
            graphic.enabled = false;
        }
    }

}
