using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JB_PlayerData
{
    public int heroType;
    public bool movable;
    public float[] position;

    public JB_PlayerData (JB_PlayerUnit player)
    {
        heroType = (int)player.heroType;
        movable = player.canMove;

        position = new float[2];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}
