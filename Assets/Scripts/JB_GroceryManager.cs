using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.Events;
public class JB_GroceryManager : MonoBehaviour
{
    public Image[] crossTickImg;
    public Sprite greenTick;

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
        switch (currentLevel)
        {
            // each level requires 3 grocery items to be picked up
            case 1:
                if (RunMyForLoop(3))
                {
                    Debug.Log("reached next level");
                    // next level
                }
                else
                {
                    Debug.LogWarning("Please find all grocery items!");
                    // alert player to find other grocery items
                }
                break;
            case 2:
                if (RunMyForLoop(6))
                {
                    // next level
                }
                else
                {
                    // alert player to find other grocery items
                }
                break;
            case 3:
                if (RunMyForLoop(9))
                {
                    // next level
                }
                else
                {
                    // alert player to find other grocery items
                }
                break;
            default:
                break;
                    
        }

    }

    private void OnLevelWasLoaded(int level)
    {
        // changing the value of current level every time we change scenes

        switch (level)
        {
            // main menu
            case 0:
                currentLevel = 0;
                break;
            // level one
            case 1:
                currentLevel = 1;
                break;
            // level two
            case 2:
                currentLevel = 2;
                break;
            // level three
            case 3:
                currentLevel = 3;
                break;
        }

    }

    private bool RunMyForLoop(int length)
    {
        int counter = 0;

        foreach(bool item in itemPickedUp)
        {
            if (item)
            {
                counter++;
            }
        }

        if(counter == length)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
