using UnityEngine;

[CreateAssetMenu]
public class EventPublisher : ScriptableObject
{
    public void WinGame() => Message.Publish(new GameWon());
    public void LostGame() => Message.Publish(new GameOver());
    public void GrantClockTime(int numSeconds) => Message.Publish(new GrantClockSeconds(numSeconds));
    public void NotifyTreasureAcquired() => Message.Publish(new TreasureAcquired());
}
