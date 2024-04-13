using UnityEngine;

public class InfoSign : MonoBehaviour
{
    [SerializeField] private string signText = null;
    [SerializeField] private bool isAncientLanguage = false;
    
    private bool isPlayerInRange = false;

    private void Update() {
        if (this.isPlayerInRange && InteractButton.IsDown()) {
            ShowInfo();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            this.isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            this.isPlayerInRange = false;
            HideInfo();
        }
    }

    public void ShowInfo() {
        if (isAncientLanguage) {
            Message.Publish(new ShowInfoSignDialog("This sign bears inscriptions in an ancient, mystical language. Its meaning eludes you."));
        } else {
            Message.Publish(new ShowInfoSignDialog(signText));
        }
    }

    public void HideInfo() {
        Message.Publish(new HideInfoSignDialog());
    }

}
