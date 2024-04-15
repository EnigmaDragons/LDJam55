using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] private bool isBlocked = false;
    [SerializeField] private bool isPouring = true;

    public bool FastCurrent => !isBlocked && isPouring;
    
    private float checkInterval = 0.25f;
    private float nextCheckTime = 0f;
    
    private void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            var blockers = CurrentGameState.GameState.WaterBlockers;
            // Log.Info($"Waterfall - Check Num Possible Blockers {blockers.Count}");
            var wasBlocked = isBlocked;
            isBlocked = blockers.AnyNonAlloc(h =>
            {
                var objLoc = new Vector2(h.transform.position.x, h.transform.position.z);
                var waterFallLoc = new Vector2(transform.position.x, transform.position.z);
                // Log.Info($"Waterfall Loc: {waterFallLoc} - Blocker Loc: {objLoc}");
                var isClose = Vector2.Distance(objLoc, waterFallLoc) <= 0.25f;
                return isClose;
            });
            ProcessChanged(wasBlocked);
            nextCheckTime = Time.time + checkInterval;
        }
    }

    private void ProcessChanged(bool stateBefore)
    {
        if (isBlocked == stateBefore)
            return;
        
        Log.Info($"Is Blocked - {isBlocked}", this);
    }
} 
