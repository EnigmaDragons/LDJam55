using System;
using UnityEngine;

public static class CurrentGameState
{
    [SerializeField] private static GameState gameState;

    public static GameState GameState
    {
        get
        {
            InitIfNeeded();
            return gameState;
        }
    }
    
    public static void InitIfNeeded()
    {
        if (gameState == null)
        {
            Init();
        }
    }
    
    public static void Init() => gameState = new GameState();
    public static void Init(GameState initialState) => gameState = initialState;
    public static void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public static void Unsubscribe(object owner) => Message.Unsubscribe(owner);
    
    public static void UpdateState(Action<GameState> apply)
    {
        InitIfNeeded();
        UpdateState(_ =>
        {
            apply(gameState);
            return gameState;
        });
    }
    
    public static void UpdateState(Func<GameState, GameState> apply)
    {
        InitIfNeeded();
        gameState = apply(gameState);
        Message.Publish(new GameStateChanged(gameState));
    }
}
