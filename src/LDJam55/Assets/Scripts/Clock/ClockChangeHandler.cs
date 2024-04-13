
public class ClockChangeHandler : OnMessage<GrantClockSeconds>
{
    protected override void Execute(GrantClockSeconds msg)
    {
        CurrentGameState.UpdateState(gs =>
        {
            gs.CurrentGameTime += msg.NumSeconds;
        });
    }
}
