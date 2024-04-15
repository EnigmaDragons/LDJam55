using System.Collections.Generic;
using UnityEngine;


public class Unsummon : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] private float secondsToDestroy;
    [SerializeField] private float secondsTilGone;

    private List<GameObject> _toDestroy;
    private float _t;
    private bool _hasDestroyed;

    private void OnEnable()
    {
        if (gameObject.layer == 8)
            return;
        _toDestroy = new List<GameObject>();
        _t = 0;
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        var SummonColliders = hitColliders;
        foreach (var collider in SummonColliders)
        {
            var summon = collider.gameObject.GetComponent<SummonInstance>();
            if (summon == null)
                continue;
            if (CurrentGameState.GameState.SummonNames.Contains(summon.summon.SummonName))
                _toDestroy.Add(collider.gameObject);
        }
    }

    private void Update()
    {
        if (gameObject.layer == 8)
            return;
        _t += Time.deltaTime;
        if (!_hasDestroyed && _t >= secondsToDestroy)
        {
            foreach (var destroyable in _toDestroy)
                Destroy(destroyable);
            _hasDestroyed = true;
        }
        else if (_t >= secondsTilGone)
            Destroy(gameObject);
    }
}
