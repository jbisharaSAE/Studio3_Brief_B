﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JB_Dialogue
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;

}
