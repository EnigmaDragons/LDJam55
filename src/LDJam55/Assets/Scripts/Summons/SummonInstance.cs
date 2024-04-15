using UnityEngine;

public class SummonInstance : MonoBehaviour
{
    [SerializeField] public Summon summon;

    private void OnEnable()
    {
        CurrentGameState.UpdateState(gs => gs.SummonInstances.Add(this));
    }

    private void OnDisable()
    {
        CurrentGameState.UpdateState(gs => gs.SummonInstances.Remove(this));
    }
}
