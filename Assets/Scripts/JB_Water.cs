using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Water : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<JB_PlayerUnit>().heroType == HeroType.Bob)
            {
                // if bob hits the water, disable collider
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
            }
        }
    }
}
