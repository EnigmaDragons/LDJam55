using UnityEngine;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool isFastWater = false;

    public bool IsFastWater(){
        return isFastWater;
    }

    public GameObject TargetPosition(){
        return targetPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
