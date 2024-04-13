
public class NotifyPlayerOnSummonLearned : OnMessage<SummonLearned>
{
    protected override void Execute(SummonLearned msg)
    {
        Message.Publish(new ShowNotification("New Summon!", $"You gained the ability to magically summon {msg.Summon.SummonName}"));
    }
}
