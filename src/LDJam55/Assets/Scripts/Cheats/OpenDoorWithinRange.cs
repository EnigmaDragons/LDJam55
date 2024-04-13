using UnityEngine;

public class OpenDoorWithinRange : MonoBehaviour
{
    public KeyCode openDoorKey = KeyCode.O; // Key to press to open the door
    public float openRange = 3.0f; // Range within which the door can be opened

    private void Update()
    {
        if (Input.GetKeyDown(openDoorKey))
        {
            CheckForDoorAndOpen();
        }
    }

    private void CheckForDoorAndOpen()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, openRange, 6);
        Log.Info($"Checking for Door - Num Collisions {hitColliders.Length}");
        foreach (var hitCollider in hitColliders)
        {
            Log.Info(hitCollider.gameObject);
            if (hitCollider.gameObject.CompareTag("Door")) // Assuming the door has a tag "Door"
            {
                Door door = hitCollider.GetComponent<Door>();
                if (door != null)
                {
                    door.Open(); // Call the Open method on the door
                    Log.Error("Door Component not found", hitCollider.gameObject);
                }
            }
        }
    }
}
