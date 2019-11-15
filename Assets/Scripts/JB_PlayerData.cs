using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JB_PlayerData
{
    public int heroType;
    public bool movable;
    public bool[] levers;
    public bool[] waterMovables;
    public bool[] waterToggles;
    public bool[] itemsPickedUp;
    public float[] position;
    

    public JB_PlayerData (JB_PlayerUnit player)
    {
        heroType = (int)player.heroType;
        movable = player.canMove;

        // initialising arrays
        position = new float[2];
        levers = new bool[player.bLevers.Count];
        waterMovables = new bool[player.waterMovable.Count];
        waterToggles = new bool[player.waterToggle.Count];
        itemsPickedUp = new bool[player.itemsPickedUp.Length];


        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        for(int i = 0; i < levers.Length; ++i)
        {
            levers[i] = player.bLevers[i];
        }

        for(int i = 0; i < itemsPickedUp.Length; ++i)
        {
            itemsPickedUp[i] = player.itemsPickedUp[i];
        }

        for(int i = 0; i < waterMovables.Length; ++i)
        {
            waterMovables[i] = player.waterMovable[i];
            waterToggles[i] = player.waterToggle[i];
        }

    }
}
