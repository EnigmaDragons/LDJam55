using UnityEngine;
using UnityEditor;

namespace RoomGen
{


    [CustomEditor(typeof(TargetGenerator))]
    public class TargetGeneratorEditor : Editor
    {

        TargetGenerator targetGenerator;


        private void OnEnable()
        {
            targetGenerator = (TargetGenerator)target;
        }





        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();


            if (GUILayout.Button("Generate"))
            {

                targetGenerator.Generate(targetGenerator.id);

            }


            if (GUI.changed)
            {
                targetGenerator.Generate(targetGenerator.id);
            }
        }
    }
}