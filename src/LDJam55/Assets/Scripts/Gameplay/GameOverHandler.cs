
using Story;
using UnityEngine;

public class GameOverHandler : OnMessage<GameWon, GameOver, CutsceneFinished, TreasureAcquired>
{
    [SerializeField] private Cutscene win;
    [SerializeField] private Cutscene lostBeforeTreasure;
    [SerializeField] private Cutscene lostAfterTreasure;

    private bool _treasureAquired;
    private bool _navigateToCredits;
    private bool _navigateToGameOver;

    private void Start()
    {
        _treasureAquired = false;
        _navigateToCredits = false;
        _navigateToGameOver = false;
    } 
    
    protected override void Execute(GameWon msg)
    {
        _navigateToCredits = true;
        Message.Publish(new PlayCutscene(win));
    }

    protected override void Execute(GameOver msg)
    {
        _navigateToGameOver = true;
        if (_treasureAquired)
            Message.Publish(new PlayCutscene(lostAfterTreasure));
        else 
            Message.Publish(new PlayCutscene(lostBeforeTreasure));
    }

    protected override void Execute(CutsceneFinished msg)
    {
        if (_navigateToCredits)
        {
            Message.Publish(new NavigateToSceneRequested("CreditsScene"));
            Reset();
        }
        if (_navigateToGameOver)
        {
            Message.Publish(new NavigateToSceneRequested("GameOverScene"));
            Reset();
        }
    }

    protected override void Execute(TreasureAcquired msg)
    {
        _treasureAquired = true;
    }

    private void Reset()
    {
        _treasureAquired = false;
        _navigateToCredits = false;
        _navigateToGameOver = false;
    }
}
