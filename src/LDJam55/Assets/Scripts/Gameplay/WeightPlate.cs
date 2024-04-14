using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeightPlate : ConstraintBase
{
    private List<GameObject> _heavies = new List<GameObject>();
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;
    
    public override bool IsSatisfied => _heavies.Count > 0;

    private void Awake()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<Heavy>() != null)
            {
                _heavies.Add(collider.gameObject);                
            }
        }
        ProcessChanged(false);
    }

    private void OnCollisionEnter(Collision collision) => ObjEnter(collision.gameObject);
    private void OnTriggerEnter(Collider other) => ObjEnter(other.gameObject);

    private void OnCollisionExit(Collision collision) => ObjExit(collision.gameObject);
    private void OnTriggerExit(Collider other) => ObjExit(other.gameObject);

    private void ObjEnter(GameObject o)
    {
        var wasPressed = IsSatisfied;
        if (o.GetComponentInChildren<Heavy>() != null)
        {
            _heavies.Add(o.gameObject);
            ProcessChanged(wasPressed);
        }
    }

    private void ObjExit(GameObject o)
    {
        var wasPressed = IsSatisfied;
        if (o.GetComponentInChildren<Heavy>() != null)
        {
            _heavies.Remove(o.gameObject);
            ProcessChanged(wasPressed);
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
