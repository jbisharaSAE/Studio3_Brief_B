using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JB_PlayerData
{
    public int heroType;
    public bool movable;

    public List<bool> levers = new List<bool>();
    public List<bool> waterMovables = new List<bool>();
    public List<bool> waterToggles = new List<bool>();
    public List<bool> itemsPickedUp = new List<bool>();

    public float[] position;
    

    public JB_PlayerData (JB_PlayerUnit player)
    {
        heroType = (int)player.heroType;
        movable = player.canMove;

        // initialising arrays
        position = new float[2];
        //levers.Clear();
        //waterMovables.Clear();
        //waterToggles.Clear();
        //itemsPickedUp.Clear();

        //levers = new bool[player.bLevers.Count];
        //waterMovables = new bool[player.waterMovable.Count];
        //waterToggles = new bool[player.waterToggle.Count];
        //itemsPickedUp = new bool[player.itemsPickedUp.Length];


        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        for(int i = 0; i < player.bLevers.Count; ++i)
        {
            levers.Add(player.bLevers[i]);
        }

        for(int i = 0; i < player.itemsPickedUp.Length; ++i)
        {
            itemsPickedUp.Add(player.itemsPickedUp[i]);
        }

        for(int i = 0; i < player.waterMovable.Count; ++i)
        {
            waterMovables.Add(player.waterMovable[i]);
            waterToggles.Add(player.waterToggle[i]);
        }

    }
}
