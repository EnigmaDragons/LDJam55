using UnityEngine;

public class PlayerController : OnMessage<SummonLearned, SummonLearningDismissed, ShowSummonMenu, HideSummonMenu>
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private MoveableObject _moveableObject;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";

    private Vector3 movement;

    public bool PlayerHasControl { get; set; } = true;

    private void Start()
       => PlayerHasControl = true;

    private void Update()
    {
        if (_moveableObject.IsMoving)
        {
            var direction = new Vector3(
                _moveableObject.targetPosition.position.x - transform.position.x,
                0,
               _moveableObject.targetPosition.position.z - transform.position.z
            ).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion smoothRotation = Quaternion.Slerp(playerRigidBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            playerRigidBody.MoveRotation(smoothRotation);
        }
        if (!PlayerHasControl || _moveableObject.IsMoving)
        {
            movement = Vector3.zero;
        }
        else
        {
            movement = new Vector3(Input.GetAxisRaw(HorizontalInput), 0.0f, Input.GetAxisRaw(VerticalInput)).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerHasControl || _moveableObject.IsMoving)
        {
            playerRigidBody.velocity = Vector3.zero;
            return;
        }

        Vector3 velocity = movement * movementSpeed;
        playerRigidBody.velocity = velocity;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion smoothRotation = Quaternion.Slerp(playerRigidBody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            playerRigidBody.MoveRotation(smoothRotation);
        }

        if (movement == Vector3.zero)
        {
            playerRigidBody.velocity = Vector3.zero;
        }
    }

    public void RotatePlayer(Quaternion quatty)
    {
        playerRigidBody.MoveRotation(quatty);
    }

    protected override void Execute(ShowSummonMenu msg)
        => PlayerHasControl = false;

    protected override void Execute(HideSummonMenu msg)
        => PlayerHasControl = true;

    protected override void Execute(SummonLearned msg)
        => PlayerHasControl = false;
    protected override void Execute(SummonLearningDismissed msg)
        => PlayerHasControl = true;
}
