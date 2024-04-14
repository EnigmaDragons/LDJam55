using UnityEditor;
using UnityEngine;

namespace RoomGen
{

    public class DoorPinMenuItem
    {

        [MenuItem("GameObject/RoomGen/Door Pin", false, 10)]
        static void CreateDoorPin(MenuCommand menuCommand)
        {
            GameObject doorPin = new GameObject("DoorPin");
            doorPin.AddComponent<DoorPin>();

            GameObjectUtility.SetParentAndAlign(doorPin, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(doorPin, "Create " + doorPin.name);
            Selection.activeObject = doorPin;
        }

    }
}