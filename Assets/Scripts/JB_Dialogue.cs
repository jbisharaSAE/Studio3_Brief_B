using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JB_Dialogue
{
    [Tooltip("Bob or Tot only")]
    public string[] names;

    [TextArea(3,10)]
    public string[] sentences;

}
