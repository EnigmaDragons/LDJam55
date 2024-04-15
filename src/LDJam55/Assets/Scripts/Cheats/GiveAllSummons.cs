using System.Linq;
using UnityEngine;

public class GiveAllSummons : MonoBehaviour
{
    [SerializeField] private Summon[] summons;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus) && Input.GetKeyDown(KeyCode.L))
        {
            Log.Info($"Cheat - Give all {summons.Length} Summons");
            foreach (var s in summons)
            {
                CurrentGameState.UpdateState(gs => gs.SummonNames = summons.Select(x => x.SummonName).ToList());   
            }
            Message.Publish(new CheatHappened());
        }
    }
}
