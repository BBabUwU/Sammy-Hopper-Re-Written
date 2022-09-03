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
        UpdateGameState(gameState);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                CanvasManager.Instance.SwitchCanvas(CanvasType.MainMenu);
                break;

            case GameState.Exploration:
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
                UIManager.Instance.TurnOnUI(UIType.PlayerUI);
                break;

            case GameState.NPCInteraction:
                UIManager.Instance.TurnOnUI(UIType.DialogueUI);
                break;

            case GameState.AnsweringQuiz:
                UIManager.Instance.TurnOnUI(UIType.QuizUI);
                break;

            case GameState.BossQuizEvaluation:
                UIManager.Instance.TurnOnUI(UIType.QuizResult);
                break;

            case GameState.BossBattle:
                UIManager.Instance.TurnOnUI(UIType.BossUI);
                break;

            case GameState.PlayerDead:
                UIManager.Instance.TurnOnUI(UIType.PlayerDeathUI);
                break;

            case GameState.LevelComplete:
                Debug.Log("GO TO NEXT LEVEL");
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState { MainMenu, Exploration, NPCInteraction, AnsweringQuiz, BossQuizEvaluation, BossBattle, PlayerDead, LevelComplete }
