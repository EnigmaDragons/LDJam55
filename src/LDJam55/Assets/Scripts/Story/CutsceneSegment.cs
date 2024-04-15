using System;
using UnityEngine;

[Serializable]
public class CutsceneSegment
{
    public Sprite Background;
    public string Caption;
    public FMODUnity.EventReference VoiceLine;
    public float Delay;
}