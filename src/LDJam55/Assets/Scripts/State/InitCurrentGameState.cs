using UnityEngine;

public sealed class InitCurrentGameState : MonoBehaviour
{
    private void Awake() => CurrentGameState.Init();
}
