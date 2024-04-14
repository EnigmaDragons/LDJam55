using System;
using System.Linq;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject torch;
    [SerializeField] private float range;
    
    private void OnEnable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        var flammableColliders = hitColliders.Where(x => x.gameObject.CompareTag("Flammable"));
        foreach (var collider in flammableColliders)
            collider.gameObject.GetComponent<Triggerable>().Trigger();
        if (flammableColliders.Any())
            Destroy(torch);
    }
}