using UnityEngine;
using UnityEngine.Events;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float epsilon = 0.01f;
    [SerializeField] private Transform objectBase;

    public UnityEvent soundEventsStart;
    public UnityEvent soundEventsStop;
    public Transform targetPosition { get; set; }
    public bool IsMoving { get; set; }

    public void SetDestination(GameObject destination)
    {
        targetPosition = destination.transform;

        var distance = Vector3.Distance(objectBase.position, targetPosition.position);
        if (distance > epsilon && !IsMoving)
        {

            IsMoving = true;
            soundEventsStart.Invoke();
        }
    }

    private void Update()
    {
        if (!IsMoving) return;


        var distance = Vector3.Distance(
            new Vector3(objectBase.position.x, 0, objectBase.position.z),
            new Vector3(targetPosition.position.x, 0, targetPosition.position.z)
            );
        if (distance > epsilon)
        {

            ForceMoveObject();
        }
        else
        {
            IsMoving = false;
            soundEventsStop.Invoke();
        }
    }


    public void ForceMoveObject()
    {
        objectBase.position = Vector3.MoveTowards(
            objectBase.position,
            new Vector3(targetPosition.position.x, objectBase.position.y, targetPosition.position.z),
            movementSpeed * Time.deltaTime
            );
    }
}
