using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_SaveManager : MonoBehaviour
{
    public GameObject playerUnit;

    private bool toggle = false;

    public void SavePlayer()
    {
        JB_SaveSystem.SavePlayer(playerUnit.GetComponent<JB_PlayerUnit>());
    }

    public void LoadPlayer()
    {
        JB_PlayerData data = JB_SaveSystem.LoadPlayer();

        playerUnit.GetComponent<JB_PlayerUnit>().canMove = data.movable;
        playerUnit.GetComponent<JB_PlayerUnit>().heroType = (HeroType)data.heroType;

        Vector2 position;

        position.x = data.position[0];
        position.y = data.position[1];

        playerUnit.transform.position = position;
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
