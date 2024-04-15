using UnityEngine;

namespace Story
{
    public class OnPlayerEnterPlayCutscene : MonoBehaviour
    {
        [SerializeField] private Cutscene cutscene;

        private bool _triggered;

        private void Start() => _triggered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered && other.CompareTag("Player"))
            {
                Message.Publish(new PlayCutscene(cutscene));
                _triggered = true;
            }
        }
    }
}