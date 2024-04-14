using UnityEngine;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool isFastWater = false;
    [SerializeField] private float movementSpeed = 8;

    private PlayerController playerController;
    private Transform objectInWater = null;

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
        var distance = Vector3.Distance(playerController.transform.position, targetPosition.transform.position);
        if (distance > 0.5)
        {
            playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, targetPosition.transform.position, movementSpeed / 20);
            playerController.RotatePlayer(Quaternion.LookRotation(targetPosition.transform.position - playerController.transform.position));
        }
    }

    private void ForceMoveObject()
    {
        var distance = Vector3.Distance(objectInWater.position, targetPosition.transform.position);
        if (distance > 0.5)
        {
            objectInWater.position = Vector3.MoveTowards(objectInWater.position, targetPosition.transform.position, movementSpeed / 20);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var incomingPlayerController = collider.GetComponentInParent<PlayerController>();
            if (incomingPlayerController?.PlayerHasControl == true && isFastWater)
            {
                playerController = incomingPlayerController;
                playerController.PlayerHasControl = false;
                print("Player Entered Trigger");
            }
        }
        else if (collider.CompareTag("Moveable"))
        {
            objectInWater = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerController.transform.rotation = new Quaternion(0, playerController.transform.rotation.y, 0, 0);
            playerController.PlayerHasControl = true;
            playerController = null;
            print("Player Left Trigger");
        }
        else if (collider.CompareTag("Moveable"))
        {
            objectInWater = null;
        }
    }
}
