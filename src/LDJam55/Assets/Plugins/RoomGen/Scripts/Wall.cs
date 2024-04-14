using System.Collections;
using System.Collections.Generic;
using RoomGen;
using UnityEngine;

namespace RoomGen
{
    [System.Serializable]
    public class Wall: Tile
    {
        [SerializeField]
        public bool allowDecor;
    }
}
