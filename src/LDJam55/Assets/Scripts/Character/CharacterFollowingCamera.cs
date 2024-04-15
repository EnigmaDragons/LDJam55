using System;
using UnityEngine;

namespace Character
{
    public class CharacterFollowingCamera : MonoBehaviour
    {
        [SerializeField] private float cameraYOffset = 15f;
        [SerializeField] private float xMin;
        [SerializeField] private float xMax;
        [SerializeField] private float yMin = 5f;
        [SerializeField] private float yMax = 50f;
        [SerializeField] private float zMin;
        [SerializeField] private float zMax;
        [SerializeField] private float zoomSpeed = 3.0f;


        private GameObject player;

        private void Start()
        {
            transform.position = new Vector3(player.transform.position.x, cameraYOffset,
                player.transform.position.z);
        }

        public void Update()
        {

            var scrollDeltaPosition = Input.mouseScrollDelta.y * -zoomSpeed;
            transform.position = new Vector3(
                Math.Clamp(player.transform.position.x, xMin, xMax),
                Math.Clamp(transform.position.y + scrollDeltaPosition, yMin, yMax), 
                Math.Clamp(player.transform.position.z, zMin, zMax)
                );
        }

        private void OnEnable()
        {
            player = GameObject.FindWithTag("Player");
        }
    }
}