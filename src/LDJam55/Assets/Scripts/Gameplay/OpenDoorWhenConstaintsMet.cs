using System;
using UnityEngine;

public class OpenDoorWhenConditionsMet : MonoBehaviour
{
    [SerializeField] private Door target;
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
        if (triggerables.AllNonAlloc(t => t.IsSatisfied))
        {
            Log.Info($"Door Opened - {triggerables.Length} Triggers were Activated", this);
            target.Open();
        }
    }
}
