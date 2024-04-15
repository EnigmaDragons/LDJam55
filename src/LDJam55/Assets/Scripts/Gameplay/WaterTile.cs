using UnityEngine;
using UnityEngine.Events;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool isFastWater = false;
    public UnityEvent soundEventsStart;
    public UnityEvent soundEventsStop;

    public bool SetIsFast(bool isFast) => isFastWater = isFast;

    private void OnTriggerStay(Collider c)
    {
        if ( isFastWater && c.TryGetComponent<MoveableObject>(out var moveableObject))
        {
            moveableObject.SetDestination(targetPosition);
        }
    }
}
