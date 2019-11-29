using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Rotator : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        //float step = speed * Time.deltaTime;

        // oscillating between two numbers - Cos(time, speed / PI) * scale - where scale determines how large the number oscillates between
        //float x = Mathf.Cos(Time.time * speed / Mathf.PI) * 1f;

        float x = Mathf.PingPong(Time.time, 1f);

        transform.localScale = new Vector3(x, 1, 1);

    }
}
