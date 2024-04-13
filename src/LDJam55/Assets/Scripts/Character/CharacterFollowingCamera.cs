using UnityEngine;

namespace Character
{
    public class CharacterFollowingCamera : MonoBehaviour
    {
        private GameObject player;
        
        private void OnEnable()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Update()
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
    }
}