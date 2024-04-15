using UnityEngine;

public class InfoSign : MonoBehaviour
{
    [SerializeField] private string signText = null;
    [SerializeField] private bool isAncientLanguage = false;
    [SerializeField] private float triggerDistance = 1.5f;
    
    private bool isPlayerInRange = false;

    private void Update()
    {
        var player = CurrentGameState.GameState.Player;
        if (player == null)
        {
            Log.Warn("Player is Null");
            return;
        }

        var beforeState = isPlayerInRange;
        var objLoc = new Vector2(player.transform.position.x, player.transform.position.z);
        var selfLoc = new Vector2(transform.position.x, transform.position.z);
        var distance = Vector2.Distance(objLoc, selfLoc);
        var isClose = distance  <= triggerDistance;
        // Log.Info($"InfoSign Player: {objLoc}. Sign: {selfLoc}. Distance: ${distance}");
        isPlayerInRange = isClose;
        if (beforeState != isClose)
        {
            if (isPlayerInRange)
                ShowInfo();
            else
                HideInfo();
        }
    }
    
    public void ShowInfo() {
        if (isAncientLanguage) {
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
}
