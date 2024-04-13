using UnityEngine;

public class OpenAllDoors : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus) && Input.GetKeyDown(KeyCode.O))
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            Log.Info($"Cheat - Open All {doors.Length} Doors");
            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().Open();
            }
        }
    }
}
