using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ArrowFollow : MonoBehaviour
{
    public Transform player;
    public float offSet;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offSet, 0f);
    }
}
