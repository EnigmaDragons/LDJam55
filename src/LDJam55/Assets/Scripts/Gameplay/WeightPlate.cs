﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeightPlate : ConstraintBase
{
    private HashSet<GameObject> _heavies = new HashSet<GameObject>();
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;
    
    public override bool IsSatisfied => _heavies.Count > 0;
    
    private float checkInterval = 0.25f;
    private float nextCheckTime = 0f;
    private float logInterval = 2f;
    private float nextLogTime = 0f;

    private void Update()
    {
        if (Time.time >= nextLogTime)
        {
            Log.Info($"Heavies present: {_heavies.Count}", this);
            nextLogTime = Time.time + logInterval;
        }

        if (Time.time >= nextCheckTime)
        {
            var heavies = CurrentGameState.GameState.Heavies;
            var wasSatisfied = IsSatisfied;
            heavies.ForEach(h =>
            {
                var isClose = Vector2.Distance(new Vector2(h.transform.position.x, h.transform.position.z), new Vector2(transform.position.x, transform.position.z)) <= 0.25f;
                if (isClose)
                {
                    _heavies.Add(h.gameObject);
                }
                else
                {
                    _heavies.Remove(h.gameObject);
                }
            });
            ProcessChanged(wasSatisfied);
            nextCheckTime = Time.time + checkInterval;
        }
    }

    private void ProcessChanged(bool stateBefore)
    {
        if (IsSatisfied == stateBefore)
            return;
        
        Log.Info($"Is Pressed - {IsSatisfied}", this);
        if (IsSatisfied)
            onPressed.Invoke();
        else
            onReleased.Invoke();
    }
}