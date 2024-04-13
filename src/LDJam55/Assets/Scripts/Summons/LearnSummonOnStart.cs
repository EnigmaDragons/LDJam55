using System.Linq;
using UnityEngine;

public class LearnSummonOnStart : MonoBehaviour
{
    [SerializeField] private Summon[] summons;

    private void Start()
        => CurrentGameState.UpdateState(x => x.SummonNames.AddRange(summons.Select(x => x.SummonName)));
}
