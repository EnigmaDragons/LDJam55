
public class SummonPlaceHolderBox : OnMessage<SummonRequested>
{
    protected override void Execute(SummonRequested msg)
        => Instantiate(msg.Summon.SummonPrefab.transform);
}

