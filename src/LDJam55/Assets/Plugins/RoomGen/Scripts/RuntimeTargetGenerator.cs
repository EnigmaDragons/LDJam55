using UnityEngine;

namespace RoomGen
{


    public class RuntimeTargetGenerator : MonoBehaviour
    {

        [Tooltip("The ID of the target generator that you want to update.")]
        public int targetGeneratorID;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RegenerateTargetGen();
        }


        void RegenerateTargetGen()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"Regenerating Target Generator with ID: {targetGeneratorID}");
                Random.InitState(System.DateTime.Now.Millisecond);
                EventSystem.instance.SetTargetGenRadius(targetGeneratorID, Random.Range(15, 30));
                EventSystem.instance.SetTargetGenObjectDensity(targetGeneratorID, Random.Range(100, 300));
                EventSystem.instance.SetTargetGenSeed(targetGeneratorID, Random.Range(0, 99999));

                EventSystem.instance.GenerateTargetGen(targetGeneratorID);
            }
        }
    }
}
