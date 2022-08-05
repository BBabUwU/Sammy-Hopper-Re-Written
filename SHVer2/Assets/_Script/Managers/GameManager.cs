using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;
    //Notify that the event has changed.
    public static event Action<GameState> OnGameStateChanged;
    public bool isUsingNotepad;
    public bool quiz1Complete;
    public bool quiz2Complete;

    private GameObject blockedArea1;
    private GameObject blockedArea2;

    private void Awake()
    {
        Instance = this;
        blockedArea1 = GameObject.FindGameObjectWithTag("PracticalProblem1");
        blockedArea2 = GameObject.FindGameObjectWithTag("PracticalProblem2");
    }

    private void Start()
    {
        UpdateGameState(GameState.Exploration);
    }

    private void CheckQuizCompletion()
    {
        if (quiz1Complete)
        {
            if (blockedArea1 != null)
            {
                Destroy(blockedArea1);
            }
        }

        if (quiz2Complete)
        {
            if (blockedArea2 != null)
            {
                Destroy(blockedArea2);
            }
        }

        UpdateGameState(GameState.Exploration);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Exploration:
                break;
            case GameState.InteractingNPC:
                break;
            case GameState.AnsweringQuiz:
                break;
            case GameState.BossBattle:
                break;
            case GameState.CompletionCheck:
                CheckQuizCompletion();
                break;
            case GameState.GameOver:
                break;
            case GameState.LevelComplete:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState { Exploration, InteractingNPC, AnsweringQuiz, CompletionCheck, BossBattle, GameOver, LevelComplete }
