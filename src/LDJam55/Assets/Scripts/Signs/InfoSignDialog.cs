using UnityEngine;

public class InfoSignDialog : OnMessage<ShowInfoSignDialog, HideInfoSignDialog>
{
    [SerializeField] private GameObject dialogPanel = null;
    [SerializeField] private TMPro.TextMeshProUGUI text = null;

    protected override void Execute(ShowInfoSignDialog msg) {
        text.text = msg.text;
        dialogPanel.SetActive(true);
    }

    protected override void Execute(HideInfoSignDialog msg) {
        dialogPanel.SetActive(false);
    }
}