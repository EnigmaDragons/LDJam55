
using UnityEngine;

namespace RoomGen
{
    [System.Serializable]
    public class Tile
    {

        [SerializeField]
        public GameObject prefab;

        [SerializeField]
        public Vector3 positionOffset;

        [SerializeField]
        public Vector3 rotationOffset;

        [SerializeField]
        [Range(0, 100)]
        public int weight;

        [SerializeField]
        public bool alignToSurface;

        [SerializeField]
        public LayerMask tileLayer;

        [SerializeField]
        [HideInInspector]
        public bool isExpanded;

    }
}