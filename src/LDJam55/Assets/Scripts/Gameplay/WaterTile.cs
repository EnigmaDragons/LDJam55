using UnityEngine;
using UnityEngine.Events;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private GameObject fastVfx;
    
    private bool isFastWater;

    private void Start()
    {
        isFastWater = true;
        fastVfx.SetActive(isFastWater);
    }
    
    public bool SetIsFast(bool isFast)
    {
        isFastWater = isFast;
        fastVfx.SetActive(isFastWater);
        return isFastWater;
    }

    private void OnTriggerStay(Collider c)
    {
        if ( isFastWater && c.TryGetComponent<MoveableObject>(out var moveableObject))
        {
            moveableObject.SetDestination(targetPosition);
        }
    }
}
