using UnityEngine;

public class InfoSign : OnMessage<GameStateChanged>
{
    [SerializeField] private string signText = null;
    [SerializeField] private bool isAncientLanguage = false;
    [SerializeField] private float triggerDistance = 1.5f;
    [SerializeField] private float maxBookDistance = 2.5f;
    
    private bool isPlayerInRange = false;

    private void Update()
    {
        var player = CurrentGameState.GameState.Player;
        if (player == null)
        {
            Log.Warn("Player is Null");
            return;
        }

        var isClose = transform.XzDistanceFromSelf(player.transform)  <= triggerDistance;
        if (isPlayerInRange != isClose)
        {
            if (isClose)
                ShowInfo();
            else
                HideInfo();
        }
        isPlayerInRange = isClose;
    }
    
    public void ShowInfo()
    {
        var anyBooksInRange = CurrentGameState.GameState.Books.AnyNonAlloc(b => transform.XzDistanceFromSelf(b.transform) < maxBookDistance);
        if (isAncientLanguage && !anyBooksInRange) {
            Debug.Log("Showing Ancient Language Sign Text");
            Message.Publish(new ShowInfoSignDialog("This sign bears inscriptions in an ancient, mystical language. Its meaning eludes you."));
        } else {
            Debug.Log("Showing Sign Info: " + signText);
            Message.Publish(new ShowInfoSignDialog(signText));
        }
    }

    public void HideInfo() {
        Debug.Log("Hiding Sign Info");
        Message.Publish(new HideInfoSignDialog());
    }

    protected override void Execute(GameStateChanged msg)
    {
        if (isAncientLanguage && isPlayerInRange)
        {
            HideInfo();
            ShowInfo();
        }
    }
}
