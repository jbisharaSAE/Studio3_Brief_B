﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_RemoveMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

}
