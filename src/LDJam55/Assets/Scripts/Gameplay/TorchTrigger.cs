using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject torch;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flammable"))
        {
            other.gameObject.GetComponent<Triggerable>().Trigger();
            Destroy(torch);
        }
    }
}