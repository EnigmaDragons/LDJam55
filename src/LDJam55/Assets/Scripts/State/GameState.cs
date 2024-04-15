using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class GameState
{
    // Should consist of only serializable primitives.
    // Any logic or non-trivial data should be enriched in CurrentGameState.
    // Except for Save/Load Systems, everything should use CurrentGameState,
    // instead of this pure data structure.
    
    // All enums used in this class should have specified integer values.
    // This is necessary to preserve backwards save compatibility.

    public float CurrentGameTime;
    public List<string> SummonNames = new();
    public HashSet<GameObject> Heavies = new();
    public HashSet<GameObject> WaterBlockers = new();
    public HashSet<GameObject> Books = new();
    public HashSet<SummonInstance> SummonInstances = new();
    public GameObject Player;
}
