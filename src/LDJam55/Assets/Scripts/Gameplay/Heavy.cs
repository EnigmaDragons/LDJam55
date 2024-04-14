using UnityEngine;

public class Heavy : MonoBehaviour 
{
    private void OnEnable()
    {
        CurrentGameState.UpdateState(gs => gs.Heavies.Add(gameObject));
    }

    private void OnDisable()
    {
        CurrentGameState.UpdateState(gs => gs.Heavies.Remove(gameObject));
    }
}
