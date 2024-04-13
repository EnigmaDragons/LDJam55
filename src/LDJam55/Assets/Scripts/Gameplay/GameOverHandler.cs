
public class GameOverHandler : OnMessage<GameWon, GameOver>
{
    protected override void Execute(GameWon msg)
    {
        Message.Publish(new NavigateToSceneRequested("CreditsScene"));
    }

    protected override void Execute(GameOver msg)
    {
        Message.Publish(new NavigateToSceneRequested("GameOverScene"));
    }
}
