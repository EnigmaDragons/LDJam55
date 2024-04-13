using UnityEngine;

public class PlayerSummonInFront : OnMessage<SummonRequested>
{
    [SerializeField] private float inFrontOffset;
    [SerializeField] private float yOffset;
    
    protected override void Execute(SummonRequested msg)
    {
        Instantiate(msg.Summon.SummonPrefab, gameObject.transform.position + new Vector3(0, yOffset, 0) + gameObject.transform.forward * inFrontOffset, Quaternion.identity, transform.parent);
    }
}