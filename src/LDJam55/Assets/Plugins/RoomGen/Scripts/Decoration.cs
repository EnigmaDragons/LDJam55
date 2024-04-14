using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomGen {

    [System.Serializable]
    public class Decoration
    {


        [SerializeField]
        public GameObject prefab;

        [SerializeField]
        public Vector3 positionOffset;

        [SerializeField]
        public Vector3 rotationOffset;

        [SerializeField]
        [Range(0, 10f)]
        public float spacingBuffer;

        [SerializeField]
        public Vector2 verticalRange;

        [SerializeField]
        public Vector2Int amountRange;

        [SerializeField]
        [Range(0, 360)]
        public float randomRotation = 0;

        [SerializeField]
        public Vector2 scaleRange;

        [SerializeField]
        [HideInInspector]
        public bool isExpanded;


    }

}
