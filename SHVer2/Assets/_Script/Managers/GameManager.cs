using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;

    [Header("State of the game")]
    public GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                //User Interface
                CanvasManager.Instance.SwitchCanvas(CanvasType.MainMenu);
                break;

            case GameState.Exploration:
                //User Interface
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
                UIManager.Instance.TurnOnUI(UIType.PlayerUI);
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

public enum GameState { MainMenu, Exploration, NPCInteraction, AnsweringQuiz, ShowScore, BossBattle, PlayerDead, LevelComplete }
