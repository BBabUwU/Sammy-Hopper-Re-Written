using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Creates an instance of the singleton, which is this class.
    public static GameManager Instance;

    //Notify that the event has changed.
    public static event Action<GameState> OnGameStateChanged;

    [Header("State of the game")]
    public GameState gameState;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Exploration);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Exploration:
                break;
            case GameState.NPCInteraction:
                break;
            case GameState.AnsweringQuiz:
                break;
            case GameState.ShowScore:
                break;
            case GameState.BossBattle:
                break;
            case GameState.PlayerDead:
                break;
            case GameState.LevelComplete:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState { Exploration, NPCInteraction, AnsweringQuiz, ShowScore, BossBattle, PlayerDead, LevelComplete }
