using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterGroup : MonoBehaviour
{
    [SerializeField] private Waterfall[] waterfalls = Array.Empty<Waterfall>();
    [SerializeField] private bool isFastCurrent = false;
    [SerializeField] private List<WaterTile> waterTiles = new List<WaterTile>();
    private void Start()
    {
        foreach (Transform child in transform)
        {
            WaterTile waterTile = child.GetComponentInChildren<WaterTile>();
            if (waterTile != null)
            {
                waterTiles.Add(waterTile);
            }
        }
    }
    
    private void Update()
    {
        var isFlowing = waterfalls.AnyNonAlloc(w => w.FastCurrent);
        if (isFastCurrent == isFlowing)
            return;

        isFastCurrent = isFlowing;
        foreach (var waterTile in waterTiles) 
            waterTile.SetIsFast(isFastCurrent);
    }
}
