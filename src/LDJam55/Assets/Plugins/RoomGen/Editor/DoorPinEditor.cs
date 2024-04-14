using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RoomGen
{

    [CustomEditor((typeof(DoorPin)))]
    public class DoorPinEditor : Editor
    {
        private Vector3 previousPosition;

        private SerializedProperty roomGenerator;

        void OnEnable()
        {
            DoorPin doorPin = (DoorPin)target;
            if (doorPin != null)
            {
                // Record the initial position of the DoorPin GameObject when the script is enabled
                previousPosition = doorPin.transform.position;
            }

            roomGenerator = serializedObject.FindProperty("roomGenerator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DoorPin doorPin = (DoorPin)target;
            if (doorPin != null && doorPin.transform.position != previousPosition)
            {
                //Regenerate the associated room generator when this pin moves.
                if (roomGenerator.objectReferenceValue != null)
                {
                    RoomGenerator roomGen = roomGenerator.objectReferenceValue as RoomGenerator;

                    if (roomGen)
                        roomGen.GenerateRoom(roomGen.id);
                }

                // Update the previous position
                previousPosition = doorPin.transform.position;
            }
        }
    }

}