using UnityEngine;

public class WaterBlocking : MonoBehaviour
{
    private void OnEnable()
    {
        CurrentGameState.UpdateState(gs => gs.WaterBlockers.Add(gameObject));
    }

    private void OnDisable()
    {
        CurrentGameState.UpdateState(gs => gs.WaterBlockers.Remove(gameObject));
    }
}
