using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;

    [Header("State of the game")]
    public GameState gameState;
    private int currentScene = 0;

    private void Awake()
    {
        CreateSingleton();
        currentScene = SceneManager.GetActiveScene().buildIndex;
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

            case GameState.SceneLoad:
                newScene(1);
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

            case GameState.VideoPlayer:
                CanvasManager.Instance.SwitchCanvas(CanvasType.VideoPlayer);
                UIManager.Instance.TurnOnUI(UIType.VideoMenu);
                break;

            case GameState.Puzzle:
                CanvasManager.Instance.SwitchCanvas(CanvasType.Puzzle);
                break;

            case GameState.LevelComplete:
                Debug.Log("GO TO NEXT LEVEL");
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void newScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);

        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            StartCoroutine("waitForSceneLoad", sceneNumber);
        }
    }

    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
            Debug.Log("Not yet loaded");
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            UpdateGameState(GameState.Exploration);
        }

        currentScene = sceneNumber;
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum GameState { MainMenu, SceneLoad, Exploration, NPCInteraction, VideoPlayer, Puzzle, AnsweringQuiz, BossQuizEvaluation, BossBattle, PlayerDead, LevelComplete }
