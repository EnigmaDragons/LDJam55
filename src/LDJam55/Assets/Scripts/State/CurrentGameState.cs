using System;
using UnityEngine;

public static class CurrentGameState
{
    [SerializeField] public static GameState GameState;

    public static void Init() => GameState = new GameState();
    public static void Init(GameState initialState) => GameState = initialState;
    public static void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public static void Unsubscribe(object owner) => Message.Unsubscribe(owner);
    
    public static void UpdateState(Action<GameState> apply)
    {
        UpdateState(_ =>
        {
            apply(GameState);
            return GameState;
        });
    }
    
    public static void UpdateState(Func<GameState, GameState> apply)
    {
        GameState = apply(GameState);
        Message.Publish(new GameStateChanged(GameState));
    }
}
