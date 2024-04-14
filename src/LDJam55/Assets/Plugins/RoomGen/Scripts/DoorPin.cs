using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoomGen
{
    /// <summary>
    /// A simple class used to mark the desired position of a manually placed door.
    /// </summary>
    public class DoorPin : MonoBehaviour
    {

        public RoomGenerator roomGenerator;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.125f);
        }
    }
}
