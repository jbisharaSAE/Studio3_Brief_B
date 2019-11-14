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

    private bool[] itemPickedUp;

    // a variable to determine what level the player is in
    private int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        itemPickedUp = new bool[11]; // one for each grocery
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
        alertText.text = "Please find remaining items on the list";
        yield return new WaitForSeconds(3.5f);
        blackTextBoxArea.SetActive(false);
        alertText.text = "";
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    // changing the value of current level every time we change scenes

    //    switch (level)
    //    {
    //        // main menu
    //        case 0:
    //            currentLevel = 0;
    //            break;
    //        // level one
    //        case 1:
    //            currentLevel = 1;
    //            break;
    //        // level two
    //        case 2:
    //            currentLevel = 2;
    //            break;
    //        // level three
    //        case 3:
    //            currentLevel = 3;
    //            break;
    //    }

    //}

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
