using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class WeightPlate : ConstraintBase
{
    private HashSet<GameObject> _heavies = new HashSet<GameObject>();
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;
    [SerializeField] private Transform button;
    [SerializeField] private float pressedY;
    [SerializeField] private float releasedY;
    [SerializeField] private float speed;
    
    public override bool IsSatisfied => _heavies.Count > 0;
    
    private float checkInterval = 0.25f;
    private float nextCheckTime = 0f;
    private float logInterval = 2f;
    private float nextLogTime = 0f;

    private void Update()
    {
        if (IsSatisfied && button.localPosition.y != pressedY)
            button.localPosition = Vector3.MoveTowards(button.localPosition, new Vector3(button.localPosition.x, pressedY, button.localPosition.z), speed * Time.deltaTime);
        else if (!IsSatisfied && button.localPosition.y != releasedY)
            button.localPosition = Vector3.MoveTowards(button.localPosition, new Vector3(button.localPosition.x, releasedY, button.localPosition.z), speed * Time.deltaTime);
            
        if (Time.time >= nextCheckTime)
        {
            var heavies = CurrentGameState.GameState.Heavies;
            var wasSatisfied = IsSatisfied;
            _heavies = new HashSet<GameObject>();
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
