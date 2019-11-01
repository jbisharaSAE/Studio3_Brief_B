using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class JB_PlayerUnit : NetworkBehaviour
{
    public HeroType heroType;

    private Rigidbody2D rb;

    private float directionX;
    public float moveSpeed = 5f;

    public override void OnStartAuthority()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.hasAuthority) { return; }

        directionX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!this.hasAuthority) { return; }

        rb.velocity = new Vector2(directionX, rb.velocity.y);
    }

}
