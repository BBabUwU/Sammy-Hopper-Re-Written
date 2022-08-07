using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Creates an instance of the singleton, which is this class.
    public static GameManager Instance;
    public GameState gameState;
    //Notify that the event has changed.
    public static event Action<GameState> OnGameStateChanged;

    //Checks if the player is using notepad.
    public bool isUsingNotepad;

    //Tracks progress of Quiz
    public bool quiz1Complete;
    public bool quiz2Complete;

    private GameObject _blockedArea1;
    private GameObject _blockedArea2;

    private void Awake()
    {
        Instance = this;
        _blockedArea1 = GameObject.FindGameObjectWithTag("PracticalProblem1");
        _blockedArea2 = GameObject.FindGameObjectWithTag("PracticalProblem2");
    }

    private void Start()
    {
        UpdateGameState(GameState.Exploration);
    }

    private void CheckQuizCompletion()
    {
        if (quiz1Complete)
        {
            if (_blockedArea1 != null)
            {
                Destroy(_blockedArea1);
            }
        }

        if (quiz2Complete)
        {
            if (_blockedArea2 != null)
            {
                Destroy(_blockedArea2);
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
