
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
        Message.Publish(new NavigateToSceneRequested("CreditsScene"));
    }

    protected override void Execute(GameOver msg)
    {
        
        Message.Publish(new NavigateToSceneRequested("GameOverScene"));
    }

    protected override void Execute(CutsceneFinished msg)
    {
        
    }

    protected override void Execute(TreasureAcquired msg)
    {
        _treasureAquired = true;
    }
}
