using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace RoomGen
{
    public class EventSystem : MonoBehaviour
    {

        private static EventSystem _instance;

        public static EventSystem instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (EventSystem)FindObjectOfType(typeof(EventSystem));
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<EventSystem>();
                        singletonObject.name = typeof(EventSystem).ToString() + " (Singleton)";
                    }
                }

                return _instance;
            }
        }

        ///The main function that will be invoked to regenerate a room. Should be called after all RoomGenerator component variables have been adjusted.
        public event Action<int> OnGenerate;

        ///Sets a new preset for the given levelNumber.
        public event Action<int, RoomPreset, int> OnSetRoomPreset;

        ///Updating these values will increase or decrease the size of the room.
        public event Action<int, int, int> OnSetGridSize;

        ///Update the room seed. This alters the doors, floors, and wall tiles and their locations. Characters and decorations will not be affected.
        public event Action<int, int, int> OnSetRoomSeed;
        ///Update the Decor seed. This alters the roof, floor, and wall decorations and their locations. The wall/floor tiles themselves will not be affected.
        public event Action<int, int, int> OnSetDecorSeed;
        ///Update the Character seed. This alters the characters and their locations.
        public event Action<int, int, int> OnSetCharacterSeed;

        public event Action<int, int, int> OnSetDoorCount;
        public event Action<int, int, int> OnSetWindowCount;
        public event Action<int, int, int> OnSetLevelHeight;

        public event Action<int, int, Vector3> OnSetLevelOffset;
        
        
        //Target Generator Event Actions
        ///The main function that will be invoked to regenerate a Target Generator. Should be called after all TargetGenerator component variables have been adjusted.
        public event Action<int> OnGenerateTarget;
        
        /// Set the radius of the target generator. 
        public event Action<int, float> OnSetTargetGenRadius;
        
        /// Set the seed of the target generator. 
        public event Action<int, int> OnSetTargetGenSeed;
        
        /// Set the object density of the target generator. 
        public event Action<int, int> OnSetTargetGenObjectDensity;
        


        private void Awake()
        {
            var instance = EventSystem.instance;
        }


        public void Generate(int id)
        {
            OnGenerate?.Invoke(id);
        }
        
        public void SetRoomPreset(int id, RoomPreset preset, int levelNumber)
        {
            OnSetRoomPreset?.Invoke(id, preset, levelNumber);
        }

        public void SetGridSize(int id, int x, int z)
        {
            OnSetGridSize?.Invoke(id, x, z);
        }

        public void SetRoomSeed(int id, int levelNumber, int seed)
        {
            OnSetRoomSeed?.Invoke(id, levelNumber, seed);
        }

        public void SetDecorSeed(int id, int levelNumber, int seed)
        {
            OnSetDecorSeed?.Invoke(id, levelNumber, seed);
        }

        public void SetCharacterSeed(int id, int levelNumber, int seed)
        {
            OnSetCharacterSeed?.Invoke(id, levelNumber, seed);
        }

        public void SetDoorCount(int id, int levelNumber, int count)
        {
            OnSetDoorCount?.Invoke(id, levelNumber, count);
        }

        public void SetWindowCount(int id, int levelNumber, int count)
        {
            OnSetWindowCount?.Invoke(id, levelNumber, count);
        }

        public void SetLevelHeight(int id, int levelNumber, int height)
        {
            OnSetLevelHeight?.Invoke(id, levelNumber, height);
        }

        public void SetLevelOffset(int id, int levelNumber, Vector3 offset)
        {
            OnSetLevelOffset?.Invoke(id, levelNumber, offset);
        }
        
        
        
        // Target Generator Events

        public void GenerateTargetGen(int id)
        {
            OnGenerateTarget?.Invoke(id);
        }
        
        public void SetTargetGenRadius(int generatorID, float radius)
        {
            OnSetTargetGenRadius?.Invoke(generatorID, radius);
        }

        public void SetTargetGenSeed(int generatorID, int seed)
        {
            OnSetTargetGenSeed?.Invoke(generatorID, seed);
        }
        
        public void SetTargetGenObjectDensity(int generatorID, int objectDensity)
        {
            OnSetTargetGenObjectDensity?.Invoke(generatorID, objectDensity);
        }
        
    }
}

