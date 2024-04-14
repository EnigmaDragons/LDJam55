using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomGen
{
    [System.Serializable]
    public class Floor : Tile
    {

        [SerializeField]
        public bool allowDecor;

        /// <summary>
        /// Each int determines a +90 degree rotation from the origin.
        /// </summary>
        [SerializeField]
        [Range(0, 3)]
        public int randomRotation = 0;

    }
}
