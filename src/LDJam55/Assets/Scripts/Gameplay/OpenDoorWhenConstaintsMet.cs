using System;
using UnityEngine;

public class OpenDoorWhenConditionsMet : MonoBehaviour
{
    [SerializeField] private Door target;
    [SerializeField] private bool doorCanBeClosedAgain = false;
    [SerializeField] private ConstraintBase[] triggerables = Array.Empty<ConstraintBase>();

    private void Awake()
    {
        if (target == null)
            target = GetComponent<Door>();
        if (target == null)
            target = GetComponentInChildren<Door>();
        if (target == null)
            Log.Error("Door not found", this);
    }

    private void Update()
    {
        var allSatisfied = triggerables.AllNonAlloc(t => t.IsSatisfied);
        if (!target.IsOpen && allSatisfied)
        {
            Log.Info($"Door Opened - {triggerables.Length} Triggers were Activated", this);
            target.Open();
        }

        if (target.IsOpen && doorCanBeClosedAgain && !allSatisfied)
        {
            Log.Info($"Door Closed - {triggerables.Length} Triggers were Not All Active", this);
            target.Close();
        }
    }
}
