using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_AdjustWater : MonoBehaviour
{
    
    public Transform pointOne;
    public Transform pointTwo;

    public Transform water;
    public Transform button;

    public float scaleLimit;

    private Vector3 initialScale;

    public bool waterToMove;
    public bool bToggle;
    Vector3 newScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = water.localScale;    
    }

    // Update is called once per frame
    void Update()
    {
        if (waterToMove)
        {
            newScale = water.localScale;

            if (bToggle)
            {
                newScale.y += Time.deltaTime;
                MoveButton(bToggle);
                // move water up
                if (newScale.y < scaleLimit)
                {
                    water.localScale = newScale;
                }
            }
            else
            {
                newScale.y -= Time.deltaTime;
                MoveButton(bToggle);
                // move water down
                if (newScale.y > initialScale.y)
                {
                    water.localScale = newScale;
                }
            }
            
        }
        
    }

    private void MoveButton(bool myBool)
    {
        if (myBool)
        {
            button.position = Vector2.MoveTowards(button.position, pointTwo.position, 5f * Time.deltaTime);
        }
        else
        {
            button.position = Vector2.MoveTowards(button.position, pointOne.position, 5f * Time.deltaTime);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<JB_PlayerUnit>().activateButton.SetActive(true);
    //        waterToMove = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<JB_PlayerUnit>().activateButton.SetActive(false);
    //        waterToMove = false;
    //    }
    //}

    private void OnEnable()
    {
        JB_PlayerUnit.OnWaterButton += AdjustWater;
        
    }

    private void OnDisable()
    {
        JB_PlayerUnit.OnWaterButton -= AdjustWater;

    }

    public void AdjustWater()
    {
        bToggle = !bToggle;
    }
}
