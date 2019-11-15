using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class JB_SaveManager : MonoBehaviour
{
    public GameObject playerUnit;

    private bool toggle = false;

    public void SavePlayer()
    {
        // check if to see if player is on pressure pad object before saving
        playerUnit.GetComponent<JB_PlayerUnit>().SendMessage("FindSceneItems");
        JB_SaveSystem.SavePlayer(playerUnit.GetComponent<JB_PlayerUnit>());
    }

    public void LoadPlayer()
    {
        JB_PlayerData data = JB_SaveSystem.LoadPlayer();


        OrganiseGroceryList(data);

        List<bool> leverArray = new List<bool>();
        List<bool> waterMoveArray = new List<bool>();
        List<bool> waterToggleArray = new List<bool>();

        foreach(bool item in data.levers)
        {
            leverArray.Add(item);
        }

        foreach(bool item in data.waterMovables)
        {
            waterMoveArray.Add(item);
        }

        foreach(bool item in data.waterToggles)
        {
            waterToggleArray.Add(item);
        }


        playerUnit.GetComponent<JB_PlayerUnit>().LoadSceneItems(leverArray, waterMoveArray, waterToggleArray);

        playerUnit.GetComponent<JB_PlayerUnit>().canMove = data.movable;

        CheckPlayerType(data.heroType);
        

        Vector2 position;

        position.x = data.position[0];
        position.y = data.position[1];

        playerUnit.transform.position = position;

        for (int i = 0; i < data.itemsPickedUp.Count; ++i)
        {
            playerUnit.GetComponent<JB_PlayerUnit>().itemsPickedUp[i] = data.itemsPickedUp[i];
        }
            
        
    }

    private void OrganiseGroceryList(JB_PlayerData data)
    {

        GameObject groceryManagerObj = GameObject.FindGameObjectWithTag("GroceryManager");
        GameObject[] groceryItems = GameObject.FindGameObjectsWithTag("Item");
        //List<bool> itemsPickedUp = new List<bool>();

        for(int i = 0; i < data.itemsPickedUp.Count; ++i)
        {
            //itemsPickedUp.Add(data.itemsPickedUp[i]);

            if (data.itemsPickedUp[i])
            {
                groceryManagerObj.GetComponent<JB_GroceryManager>().itemPickedUp[i] = false;
                groceryManagerObj.GetComponent<JB_GroceryManager>().SwapGreenTick((GroceryList)i);

                foreach (GameObject grocery in groceryItems)
                {
                    if((GroceryList)i == grocery.GetComponent<JB_GroceryItem>().groceryType)
                    {
                        Destroy(grocery);
                    }
                }
            }
        }
    }

    private void CheckPlayerType(int hType)
    {
        HeroType heroType = (HeroType)hType;

        GameObject[] all = GameObject.FindGameObjectsWithTag(this.tag);

        if (all.Length > 1)
        {
            for(int i = 0; i < all.Length; ++i)
            {
                if(heroType == all[i].GetComponent<JB_PlayerUnit>().heroType)
                {
                    ChangePlayerType(hType);
                }
            }
        }
        else
        {
            // if there are no other players in scene
            playerUnit.GetComponent<JB_PlayerUnit>().heroType = (HeroType)hType;
        }
        
    }

    private void ChangePlayerType(int hType)
    {
        // this swaps the player type if one or the other already exists in scene
        switch (hType)
        {
            case 0:
                playerUnit.GetComponent<JB_PlayerUnit>().heroType = HeroType.Tot;
                break;
            case 1:
                playerUnit.GetComponent<JB_PlayerUnit>().heroType = HeroType.Bob;
                break;
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePauseMenu(GameObject menu)
    {
        toggle = !toggle;

        menu.SetActive(toggle);
    }
}
