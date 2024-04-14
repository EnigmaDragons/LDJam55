using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen {
    [System.Serializable]
    public class Node
    {

        public Vector3 position;
        public Quaternion rotation;
        public TileType tileType;
        public bool isAvailable = true;
        public int levelNumber;

        public Node(Vector3 position_, Quaternion rotation_, TileType tileType_, int levelNumber_)
        {
            position = position_;
            rotation = rotation_;
            tileType = tileType_;
            levelNumber = levelNumber_;
        }

    }
}