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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Music_volume");
        UpdateGameState(gameState);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;

            case GameState.Exploration:
                Actions.setInventory?.Invoke(true);
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
                UIManager.Instance.TurnOnUI(UIType.PlayerUI);
                break;

            case GameState.NPCInteraction:
                UIManager.Instance.TurnOnUI(UIType.DialogueUI);
                break;

            case GameState.AnsweringQuiz:
                Actions.Switch_Camera?.Invoke(CameraType.PlayerCamera);
                Actions.Set_DefaultCamera?.Invoke(CameraType.PlayerCamera);
                UIManager.Instance.TurnOnUI(UIType.QuizUI);
                Actions.setInventory?.Invoke(false);
                break;

            case GameState.AnsweringGraph:
                UIManager.Instance.DisableAllUI();
                Actions.Switch_Camera?.Invoke(CameraType.Graphing);
                Actions.Set_DefaultCamera?.Invoke(CameraType.Graphing);
                break;

            case GameState.BossBattle:
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
                UIManager.Instance.TurnOnUI(UIType.PlayerUI);
                UIManager.Instance.TurnOnUI(UIType.BossUI);
                break;

            case GameState.BossDefeated:
                CanvasManager.Instance.SwitchCanvas(CanvasType.BossDefeated);
                break;

            case GameState.PlayerDead:
                UIManager.Instance.TurnOnUI(UIType.PlayerDeathUI);
                break;

            case GameState.VideoPlayer:
                CanvasManager.Instance.SwitchCanvas(CanvasType.VideoPlayer);
                UIManager.Instance.TurnOnUI(UIType.VideoMenu);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    MainMenu,
    Exploration,
    NPCInteraction,
    VideoPlayer,
    AnsweringQuiz,
    AnsweringGraph,
    BossBattle,
    BossDefeated,
    PlayerDead,
}
