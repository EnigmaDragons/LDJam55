using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{
    [System.Serializable]
    public class Door: Tile
    {
        [SerializeField]
        public bool allowDecor;
    }
}