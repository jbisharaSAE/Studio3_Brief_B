using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// linen, timber, bread
public enum GroceryList { Banana, Battery, Meat, Milk, Toothpaste, Soap, Pillow, Ruler, Bread }
public class JB_GroceryItem : MonoBehaviour
{
    public delegate void ItemPickup(GroceryList itemType);
    public static event ItemPickup onPickup;

    public GroceryList groceryType;

    [HideInInspector]
    public int numConversion;

    

    // Start is called before the first frame update
    void Start()
    {
        numConversion = (int)groceryType;
    
    }

    // Update is called once per frame
    void Update()
    {

        // oscillating between two numbers - Cos(time, speed / PI) * scale - where scale determines how large the number oscillates between
        float z = Mathf.Cos(Time.time * 20 / Mathf.PI) * 0.5f;

        transform.Rotate(0f, 0f, z);
    }


}

