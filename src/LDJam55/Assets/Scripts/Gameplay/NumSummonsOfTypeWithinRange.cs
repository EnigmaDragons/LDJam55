using System.Linq;
using UnityEngine;

public class NumSummonsOfTypeWithinRange : ConstraintBase
{
    [SerializeField] private Summon summon;
    [SerializeField] private int numRequired = 1;
    [SerializeField] private float maxDistance = 5f;

    private bool _isSatisfied = false;

    public override bool IsSatisfied => _isSatisfied;

    private void Update()
    {
        var matchingSummons = CurrentGameState.GameState.SummonInstances.Where(s => s.summon == summon);
        var summonsOfTypeInRange = matchingSummons.Count(s => transform.XzDistanceFromSelf(s.transform) <= maxDistance);
        _isSatisfied = summonsOfTypeInRange >= numRequired;
    }
}
