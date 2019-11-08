using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ExitEvent : MonoBehaviour
{
    public delegate void NextLevelAction();
    public static event NextLevelAction OnNextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnNextLevel();
        }
    }
}
