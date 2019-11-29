using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// linen, timber, bread
public enum GroceryList { Banana, Battery, Meat, Milk, Toothpaste, Soap, Pillow, Ruler }
public class JB_GroceryItem : MonoBehaviour
{
    public delegate void ItemPickup(GroceryList itemType);
    public static event ItemPickup onPickup;

    public GroceryList groceryType;

    private int numConversion;
    

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<JB_PlayerUnit>().AddItem(numConversion);

            //onPickup(groceryType);
            
            Destroy(gameObject);
        }
    }
}
