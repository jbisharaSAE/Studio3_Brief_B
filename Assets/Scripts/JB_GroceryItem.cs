using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroceryList { Banana, Deli, Bread, Milk, Cheese }
public class JB_GroceryItem : MonoBehaviour
{
    public GroceryList groceryType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mathf.Cos(time * speed / Mathf.PI) * scale;
        //float z = (Mathf.PingPong(Time.time, 2f) - 1f);
        //transform.localScale += new Vector3(z, z, 1f);

        // oscillating between two numbers - Cos(time, speed / PI) * scale - where scale determines how large the number oscillates between
        float z = Mathf.Cos(Time.time * 20 / Mathf.PI) * 0.5f;
        
        transform.Rotate(0f, 0f, z);
    }
}
