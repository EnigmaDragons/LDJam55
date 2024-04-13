using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
  [SerializeField] private UnityEvent onInteract;
  [SerializeField] private float interactRange = 1.0f;
  
  private bool _isPlayerInRange = false;

  private void Awake()
  {
      Log.Info("Interactable", this);
  }

  private void OnTriggerEnter(Collider other)
  {
      Log.Info("Trigger Enter", this);
      if (other.gameObject.CompareTag("Player"))
      {
          _isPlayerInRange = true;
          Log.Info("Interact - In Range", this);
      }
  }


  private void OnTriggerExit(Collider other)
  {
      Log.Info("Trigger Exit", this);
      if (other.gameObject.CompareTag("Player"))
      {
          _isPlayerInRange = false;
          Log.Info("Interact - Out of Range", this);
      }
  }

  private void Update()
  {
    if(!InteractButton.IsDown())
      return;

    if (_isPlayerInRange)
    {          
      Log.Info("Interact - Button", this);
      onInteract.Invoke();
    } else {
      BruteForceInvokeIfPlayerInRangeRange();
    }
  }

  private void BruteForceInvokeIfPlayerInRangeRange() 
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
    if(player == null) return; // If no player found, exit

    float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
    if(distanceToPlayer <= interactRange && InteractButton.IsDown())
    {
        Log.Info("Interact - Button", this);
        onInteract.Invoke();
    }
  }
}
