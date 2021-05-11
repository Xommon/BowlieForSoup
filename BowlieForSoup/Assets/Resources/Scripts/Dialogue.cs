using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string nameOfSpeaker;
    [TextArea(5, 20)]
    public string[] sentences;
}
