using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{
    [System.Serializable]
    public class Level
    {

        [SerializeField] public RoomPreset preset;

        [Range(1, 25)]
        [SerializeField]
        public int levelHeight;

        [SerializeField]
        public Vector3 levelOffset;

        [Range(0, 50)]
        [SerializeField]
        public int numDoors;

        [Range(0, 50)]
        [SerializeField]
        public int numWindows;

        [Range(1, 25)]
        [SerializeField]
        public int windowHeight;

        [Range(0, 99999)]
        [SerializeField]
        public int characterSeed;

        [Range(0, 99999)]
        [SerializeField]
        public int roomSeed;

        [Range(0, 99999)]
        [SerializeField]
        public int decorSeed;


        [HideInInspector]
        [SerializeField]
        public bool isExpanded;


        public Level(RoomPreset preset_, int levelHeight_, Vector3 levelOffset_, int numDoors_, int numWindows_, int windowHeight_, int characterSeed_, int roomSeed_, int decorSeed_)
        {
            preset = preset_;
            levelHeight = levelHeight_;
            levelOffset = levelOffset_;
            numDoors = numDoors_;
            numWindows = numWindows_;
            windowHeight = windowHeight_;
            characterSeed = characterSeed_;
            roomSeed = roomSeed_;
            decorSeed = decorSeed_;
        }

    }
}
