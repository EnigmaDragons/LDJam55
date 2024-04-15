using UnityEngine;

public class OnTreasurePlayCutscene : OnMessage<TreasureAcquired>
{
    [SerializeField] private Cutscene cutscene;
    
    protected override void Execute(TreasureAcquired msg)
    {
        Message.Publish(new PlayCutscene(cutscene));
    }
}