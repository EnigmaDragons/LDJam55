using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{

    /// <summary>
    /// This class is a simple example of how you can use the RoomGen event system to update or change RoomGen instances in your scene at runtime. These can be called from anywhere in your scripts,
    /// simply specify the ID of the generator you want to update, then pass the parameters. **IMPORTANT** you must call EventSystem.instance.Generate() to apply your changes.
    /// </summary>
    public class RuntimeGenerator : MonoBehaviour
    {

        ///The ID of the RoomGen instance you want to update.
        public int id;

        public List<RoomPreset> presets = new List<RoomPreset>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RegenerateRoom();
        }


        void RegenerateRoom()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                EventSystem.instance.SetGridSize(id, Random.Range(4, 8), Random.Range(10, 12));
                EventSystem.instance.SetRoomPreset(id, presets[Random.Range(0, presets.Count)], 0); // Sets a random preset for this generator at level 0.
                EventSystem.instance.SetRoomSeed(id, levelNumber: 0, Random.Range(0, 99999));
                EventSystem.instance.SetDecorSeed(id, 0, Random.Range(0, 99999));
                EventSystem.instance.SetCharacterSeed(id, 0, Random.Range(0, 99999));
                EventSystem.instance.SetDoorCount(id, 0, Random.Range(2, 8));
                EventSystem.instance.SetWindowCount(id, 0, Random.Range(5, 10));
                EventSystem.instance.SetLevelOffset(id, 0, new Vector3(0, 5, 0));
                EventSystem.instance.SetLevelHeight(id, 0, Random.Range(1, 4));

                //This must be called to apply the runtime changes!
                EventSystem.instance.Generate(id);
            }
        }
    }
}