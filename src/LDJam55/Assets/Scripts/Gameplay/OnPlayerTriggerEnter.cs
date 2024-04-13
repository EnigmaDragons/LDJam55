using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent delayedEvent;
    [SerializeField] private float delaySeconds = 1.5f;
    [SerializeField] private bool canRetrigger = false;

    private bool _triggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;
        
        Log.Info("Trigger Enter", this);
        if (other.gameObject.CompareTag("Player"))
        {
            if (!canRetrigger)
                _triggered = true;
            onTriggerEnter.Invoke();
            this.ExecuteAfterDelay(() => delayedEvent.Invoke(), delaySeconds);
        }
    }
}
