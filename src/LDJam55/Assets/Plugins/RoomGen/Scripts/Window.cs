using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{
    [System.Serializable]
    public class Window : Tile
    {
        [SerializeField]
        public bool allowDecor;
    }
}