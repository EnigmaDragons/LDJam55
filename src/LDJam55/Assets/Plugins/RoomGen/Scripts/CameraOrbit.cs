using UnityEngine;

namespace RoomGen
{

    public class CameraOrbit : MonoBehaviour
    {


        public Transform target;
        [Range(0, 20)] public float orbitSpeed;
        [Range(0f, 20f)] public float distanceOffset;

        public bool orbit = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (orbit)
            {
                Orbit();
            }

        }

        void Orbit()
        {
            transform.LookAt(target);
            transform.RotateAround(target.position + (transform.forward * distanceOffset), Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}