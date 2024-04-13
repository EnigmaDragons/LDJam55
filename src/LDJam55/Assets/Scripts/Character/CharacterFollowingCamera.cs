using System;
using UnityEngine;

namespace Character
{
    public class CharacterFollowingCamera : MonoBehaviour
    {
        [SerializeField] private float xMin;
        [SerializeField] private float xMax;
        [SerializeField] private float zMin;
        [SerializeField] private float zMax;

        private GameObject player;
        
        private void OnEnable()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Update()
        {
            transform.position = new Vector3(Math.Clamp(player.transform.position.x, xMin, xMax), transform.position.y, Math.Clamp(player.transform.position.z, zMin, zMax));
        }
    }
}