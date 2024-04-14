using UnityEngine;

public class InfoSignDialog : OnMessage<ShowInfoSignDialog, HideInfoSignDialog>
{
  
    [SerializeField] private GameObject panel;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    protected override void Execute(ShowInfoSignDialog msg) {
        text.text = msg.text;
        panel.SetActive(true);
        text.gameObject.SetActive(true);
    }

    protected override void Execute(HideInfoSignDialog msg) {
        panel.SetActive(false);
        text.gameObject.SetActive(false);
    }
}