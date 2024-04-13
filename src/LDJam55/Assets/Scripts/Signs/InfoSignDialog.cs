using UnityEngine;

public class InfoSignDialog : OnMessage<ShowInfoSignDialog, HideInfoSignDialog>
{
    [SerializeField] private GameObject dialogCanvas = null;
    [SerializeField] private TMPro.TextMeshProUGUI text = null;

    protected override void Execute(ShowInfoSignDialog msg) {
        text.text = msg.text;
        dialogCanvas.SetActive(true);
    }

    protected override void Execute(HideInfoSignDialog msg) {
        dialogCanvas.SetActive(false);
    }
}