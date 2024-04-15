using UnityEngine;

public class PlayerSummonInFront : OnMessage<SummonRequested>
{
    [SerializeField] private float inFrontOffset;
    [SerializeField] private float yOffset;
    
    protected override void Execute(SummonRequested msg)
    {
        var t = gameObject.transform;
        var summonPosition = t.position + new Vector3(0, yOffset, 0) + t.forward * inFrontOffset;
        
        var snappedPos = new Vector3(
             Mathf.Round(summonPosition.x / 2) * 2,
             msg.Summon.SummonAtFixedYPosition ? msg.Summon.FixedYPosition : summonPosition.y,
             Mathf.Round(summonPosition.z / 2) * 2);
        var finalPos = new Vector3(
            summonPosition.x,
            msg.Summon.SummonAtFixedYPosition ? msg.Summon.FixedYPosition : summonPosition.y,
            summonPosition.z);
        Instantiate(msg.Summon.SummonPrefab, msg.Summon.SummonToSnappedPosition ? snappedPos : finalPos, Quaternion.identity, transform.parent);
    }
}
