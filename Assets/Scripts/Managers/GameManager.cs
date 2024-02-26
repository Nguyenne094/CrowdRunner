using System;
using UnityEngine;

/// <summary>
/// Manage game states
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public enum GameState{
        Menu, 
        Game, 
        LevelComplete,
        Gameover
    }

    public enum FPS {
        MaxFPS = 0,
        Limit500 = 500,
        Limit240 = 240,
        Limit120 = 120,
        Limit60 = 60,
        Limit30 = 30
    }

    public GameState gameState;
    public FPS fps;
    public static event Action<GameState> onGameStateChanged; 

    //* DEBUG *//
    [TextArea]
    public string CurrentState;

    void Awake()
    {
        Application.targetFrameRate = (int)fps;
        SetMenuState();
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetMenuState(){
        this.gameState = GameState.Menu;
        CurrentState = gameState.ToString();

        onGameStateChanged?.Invoke(gameState);
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        CurrentState = gameState.ToString();

        onGameStateChanged?.Invoke(state);
    }

    public bool IsGameState(){
        return gameState == GameState.Game;
    }
}
