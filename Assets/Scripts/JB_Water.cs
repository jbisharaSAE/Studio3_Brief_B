using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Water : MonoBehaviour
{
    private BuoyancyEffector2D water;

    private void Start()
    {
        water = GetComponent<BuoyancyEffector2D>();

    }

    private void Update()
    {
        // gives water floating effect
        water.surfaceLevel = 2.35f + (Mathf.Cos(Time.time * 20 / Mathf.PI) * 0.1f);
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
    }
}
