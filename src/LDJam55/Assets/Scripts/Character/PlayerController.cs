using KinematicCharacterController.Walkthrough.BasicMovement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float rotationSpeed = 5;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";

    private Vector3 movement;

    private void Update()
    {
        movement = new Vector3(Input.GetAxisRaw(HorizontalInput), 0, Input.GetAxisRaw(VerticalInput)) * movementSpeed;

        HandleRotation();
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
        playerRigidBody.velocity = movement;
    }
}
