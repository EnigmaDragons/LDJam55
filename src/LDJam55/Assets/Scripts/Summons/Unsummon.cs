using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Unsummon : MonoBehaviour
{

    [SerializeField] private float range;

    private void OnEnable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        var SummonColliders = hitColliders;
        foreach (var collider in SummonColliders)
        {
            var summon = collider.gameObject.GetComponent<SummonInstance>();
            if (summon == null)
                continue;
            if (CurrentGameState.GameState.SummonNames.Contains(summon.summon.SummonName))
                Destroy(collider.gameObject);
        }
                
        Destroy(gameObject);
    }
}
