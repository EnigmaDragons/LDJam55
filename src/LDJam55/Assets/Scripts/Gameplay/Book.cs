using UnityEngine;

public class Book : MonoBehaviour
{
    private void OnEnable()
    {
        CurrentGameState.UpdateState(gs => gs.Books.Add(gameObject));
    }
    
    private void OnDisable()
    {
        CurrentGameState.UpdateState(gs => gs.Books.Remove(gameObject));
    }
}
