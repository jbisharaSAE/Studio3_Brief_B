using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Water : MonoBehaviour
{
    private BuoyancyEffector2D water;
    private float scaleY;

    private void Start()
    {
        water = GetComponent<BuoyancyEffector2D>();

        scaleY = transform.localScale.y;

        Debug.Log(scaleY);
    }

    private void Update()
    {
        // gives water floating effect
        water.surfaceLevel = scaleY + (Mathf.Cos(Time.time * 20 / Mathf.PI) * 0.1f);
    }

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
        else if(collision.gameObject.tag == "Player")
        {
            
            if (collision.gameObject.GetComponent<JB_PlayerUnit>().heroType == HeroType.Tot)
            {
                // if tot hits the water, let her have the ability to jump
                collision.gameObject.GetComponent<JB_PlayerUnit>().isGrounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<JB_PlayerUnit>().heroType == HeroType.Tot)
            {
                // when tot jumps, disable boolean so player cannot double jump
                collision.gameObject.GetComponent<JB_PlayerUnit>().isGrounded = false;
            }
        }
    }
}
