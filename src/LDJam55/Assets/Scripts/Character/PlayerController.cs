using UnityEngine;

public class PlayerController : OnMessage<ShowSummonMenu, HideSummonMenu>
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float rotationSpeed = 5;

    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";

    private Vector3 movement;

    public bool PlayerHasControl { get; set; } = true;

    private void Start()
       => PlayerHasControl = true;

    private void Update()
    {
        if (!PlayerHasControl)
        {
            return;
        }
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
        if (!PlayerHasControl)
        {
            return;
        }
        movement = new Vector3(Input.GetAxisRaw(HorizontalInput), 0, Input.GetAxisRaw(VerticalInput));
        playerRigidBody.MovePosition(transform.position + movementSpeed * Time.deltaTime * movement);
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
