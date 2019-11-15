using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //playerUnit.GetComponent<JB_PlayerUnit>().LoadSceneItems(data.levers,)

        playerUnit.GetComponent<JB_PlayerUnit>().canMove = data.movable;
        playerUnit.GetComponent<JB_PlayerUnit>().heroType = (HeroType)data.heroType;

        Vector2 position;

        position.x = data.position[0];
        position.y = data.position[1];

        playerUnit.transform.position = position;

        for (int i = 0; i < data.itemsPickedUp.Length; ++i)
        {
            playerUnit.GetComponent<JB_PlayerUnit>().itemsPickedUp[i] = data.itemsPickedUp[i];
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
