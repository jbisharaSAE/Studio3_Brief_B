using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_CameraFollowPlayer : MonoBehaviour
{
    public GameObject target;

    public float leftClamp;
    public float rightClamp;
    public float topClamp;
    public float bottomClamp;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {

            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
            
        }
            
    }
}
