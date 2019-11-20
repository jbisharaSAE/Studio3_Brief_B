using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
using TMPro;

public class JB_GroceryManager : MonoBehaviour
{
    
    public Image[] crossTickImg;
    public Sprite greenTick;

    public GameObject blackTextBoxArea;
    public TextMeshProUGUI alertText;

    public bool[] itemPickedUp;

    // a variable to determine what level the player is in
    private int currentLevel = 1;

    public static int numberOfItems;


    // Start is called before the first frame update
    void Start()
    {
        alertText.text = "Please find remaining items on the list";
        numberOfItems = 9;
        itemPickedUp = new bool[numberOfItems]; // one for each grocery
    }

    private void OnEnable()
    {
        JB_ExitEvent.OnNextLevel += TestItemsPickedUp;
        JB_GroceryItem.onPickup += SwapGreenTick;
    }

    private void OnDisable()
    {
        JB_ExitEvent.OnNextLevel -= TestItemsPickedUp;
        JB_GroceryItem.onPickup -= SwapGreenTick;
    }

    public void SwapGreenTick(GroceryList item)
    {
        // this method swaps out red cross for green tick to indicate to player they have picked up the item

        int index = (int)item;

        crossTickImg[index].sprite = greenTick;
        itemPickedUp[index] = true;

        Debug.Log("Swap function called");
    }

    public void TestItemsPickedUp()
    {
        // testing to see if player has the 3 items in the level to proceed to next level
        if (RunMyForLoop())
        {
            // level complete
        }
        else
        {
            // alert player to find all items
            StartCoroutine(CoAlertPlayer());

            
        }

    }

    IEnumerator CoAlertPlayer()
    {
        // turning on and off the alert text box area
        blackTextBoxArea.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        blackTextBoxArea.SetActive(false);

    }

    private bool RunMyForLoop()
    {
        bool allTrue = itemPickedUp.All(x => x);

        // if all booleans are true (items are picked up)
        if (allTrue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
