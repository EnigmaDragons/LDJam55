using KinematicCharacterController.Walkthrough.BasicMovement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float rotationSpeed = 5;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";
    private bool playerHasControl = true;

    private Vector3 movement;
    private GameObject targetPosition;

    private void Update()
    {
        if (!playerHasControl && targetPosition != null)
        {
            HandleLossOfControl();
            return;
        }
        HandleRotation();
    }

    private void HandleLossOfControl()
    {
        var distance = Vector3.Distance(transform.position, targetPosition.transform.position);
        if (distance > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, movementSpeed / 20);
            playerRigidBody.MoveRotation(Quaternion.LookRotation(targetPosition.transform.position - transform.position));
        }
        else
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
            playerHasControl = true;
            targetPosition = null;
        }
    }

    private void HandleRotation()
    {
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }

    private void FixedUpdate()
    {
        if (!playerHasControl)
        {
            return;
        }
        movement = new Vector3(Input.GetAxisRaw(HorizontalInput), 0, Input.GetAxisRaw(VerticalInput));
        playerRigidBody.MovePosition(transform.position + movementSpeed * Time.deltaTime * movement);
    }

    private void OnTriggerEnter(Collider collider)
    {
        HandleWaterCollision(collider);
    }

    private void HandleWaterCollision(Collider collider)
    {
        if (collider.CompareTag("Water"))
        {
            if (collider.TryGetComponent<WaterTile>(out WaterTile waterTile))
            {
                if (waterTile.IsFastWater())
                {
                    playerHasControl = false;
                    targetPosition = waterTile.TargetPosition();
                    movement = Vector3.zero;
                }
            }
        }
    }
}
