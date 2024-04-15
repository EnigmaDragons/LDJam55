using System.Linq;
using UnityEngine;

public class PlayerSummonInFront : OnMessage<SummonRequested, ShowSummonMenu>
{
    [SerializeField] private float inFrontOffset;
    [SerializeField] private float yOffset;
    
    protected override void Execute(SummonRequested msg)
    {
        var t = gameObject.transform;
        var summonPosition = t.position + new Vector3(0, yOffset, 0) + t.forward * inFrontOffset;
        
        if (msg.Summon.SummonToSnappedPosition)
        {
            var snappedPosCheck = new Vector3(
                Mathf.Round(summonPosition.x / 2) * 2,
                0.5f,
                Mathf.Round(summonPosition.z / 2) * 2);
            if (Physics.OverlapSphere(snappedPosCheck, 0.1f).Any(x => !x.isTrigger))
            {
                Message.Publish(new ShowError("This summon was blocked by an object"));
                return;
            }
            var snappedPos = new Vector3(
                Mathf.Round(summonPosition.x / 2) * 2,
                msg.Summon.SummonAtFixedYPosition ? msg.Summon.FixedYPosition : summonPosition.y,
                Mathf.Round(summonPosition.z / 2) * 2);
            Instantiate(msg.Summon.SummonPrefab, snappedPos, Quaternion.identity, transform.parent);
        }
        else
        {
            var finalPos = new Vector3(
                summonPosition.x,
                msg.Summon.SummonAtFixedYPosition ? msg.Summon.FixedYPosition : summonPosition.y,
                summonPosition.z);
            Instantiate(msg.Summon.SummonPrefab, finalPos, Quaternion.identity, transform.parent);
        }
    }

    protected override void Execute(ShowSummonMenu msg)
    {
        var t = gameObject.transform;
        var summonPosition = t.position + new Vector3(0, 0.5f, 0) + t.forward * inFrontOffset;
        if (Physics.OverlapSphere(summonPosition, 0.1f).Any(x => x.CompareTag("Wall")))
        {
            Message.Publish(new HideSummonMenu());
            Message.Publish(new ShowError("Can't summon inside of walls"));
        }
    }
}
