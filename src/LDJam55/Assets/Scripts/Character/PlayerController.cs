using UnityEngine;

public class PlayerController : OnMessage<ShowSummonMenu, HideSummonMenu>
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";

    private Vector3 movement;

    public bool PlayerHasControl { get; set; } = true;
    public Transform ForceMover { get; set; } = null;

    private void Start()
       => PlayerHasControl = true;

    private void Update()
    {
        if (!PlayerHasControl) {
            movement = Vector3.zero;
        } else {
            movement = new Vector3(Input.GetAxisRaw(HorizontalInput), 0.0f, Input.GetAxisRaw(VerticalInput)).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerHasControl)
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

        if(movement ==  Vector3.zero)
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
}
