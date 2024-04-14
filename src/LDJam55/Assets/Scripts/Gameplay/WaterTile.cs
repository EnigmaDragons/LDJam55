using UnityEngine;
using UnityEngine.Events;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool isFastWater = false;
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float epsilon = 0.01f;

    private PlayerController playerController;
    private Transform objectInWater = null;

    public UnityEvent soundEventsStart;
    public UnityEvent soundEventsStop;

    public bool IsFastWater()
    {
        return isFastWater;
    }

    public GameObject TargetPosition()
    {
        return targetPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerController != null && playerController.ForceMover != transform)
        {
            // Detach if something else wants to move the player
            playerController = null;
            soundEventsStop.Invoke();
        }
        
        if (playerController != null)
        {
            ForceMovePlayer();
        }

        if (objectInWater != null && isFastWater)
        {
            ForceMoveObject();
        }
    }

    private void ForceMovePlayer()
    {
        Log.Info("Water - Moving Player", this);
        var distance = Vector3.Distance(playerController.transform.position, targetPosition.transform.position);
        if (distance > epsilon)
        {
            playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, targetPosition.transform.position, movementSpeed / 20);
            playerController.RotatePlayer(Quaternion.LookRotation(targetPosition.transform.position - playerController.transform.position));
        }
        else
        {
            PlayerExitTile();
        }
    }

    private void ForceMoveObject()
    {
        Log.Info("Water - Moving Object", this);
        var distance = Vector3.Distance(objectInWater.position, targetPosition.transform.position);
        if (distance > epsilon)
        {
            objectInWater.position = Vector3.MoveTowards(objectInWater.position, targetPosition.transform.position, movementSpeed / 20);
        }
        else
        {
            objectInWater = null;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        Log.Info("Water - Collide Enter", c);
        if (c.CompareTag("Player"))
        {
            playerController = c.GetComponentInParent<PlayerController>();
            PlayerEnterTile();
        }
        else if (c.CompareTag("Moveable"))
        {
            objectInWater = c.transform;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        Log.Info("Water - Collide Exit", c);
        if (c.CompareTag("Player"))
        {
            PlayerExitTile();
        }
        else if (c.CompareTag("Moveable"))
        {
            objectInWater = null;
        }
    }

    private void PlayerEnterTile()
    {
        if (isFastWater)
        {
            playerController.PlayerHasControl = false;
            playerController.ForceMover = transform;
        }
        soundEventsStart.Invoke();
    }
    
    private void PlayerExitTile()
    {
        if (playerController == null)
            return;
        
        Log.Info("Water - Player Ride Finished", this);
        playerController.transform.rotation = new Quaternion(0, playerController.transform.rotation.y, 0, 0);
        playerController.PlayerHasControl = true;
        playerController.ForceMover = null;
        playerController = null;
        soundEventsStop.Invoke();
    }
}
