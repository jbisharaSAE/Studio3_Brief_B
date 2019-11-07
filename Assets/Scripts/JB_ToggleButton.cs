using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_ToggleButton : MonoBehaviour
{
    private bool toggle = false;

    public void ToggleGameObject(GameObject obj)
    {
        toggle = !toggle;

        obj.SetActive(toggle);
    }
}
