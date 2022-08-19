using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject _notepadUI;
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _quizUI;
    [SerializeField] private GameObject _npcUI;
    private GameState currentState;

    private void EnableNotepadUI()
    {
        DisableAllUI();
        _notepadUI.SetActive(true);
    }

    private void SetUIAccordingToGameState()
    {

        if (currentState == GameState.Exploration)
        {
            DisableAllUI();
            _playerUI.SetActive(true);
        }

        if (currentState == GameState.NPCInteraction)
        {
            _npcUI.SetActive(true);
        }

        if (currentState == GameState.AnsweringQuiz)
        {
            _quizUI.SetActive(true);
        }
    }

    private void DisableAllUI()
    {
        _notepadUI.SetActive(false);
        _playerUI.SetActive(false);
        _quizUI.SetActive(false);
        _npcUI.SetActive(false);
    }

    //Event functions
    //Listening to game manager.
    private void GameManagerOnGameStateChanged(GameState state)
    {
        UpdateState(state);
        SetUIAccordingToGameState();
    }

    private void UpdateState(GameState state)
    {
        currentState = state;
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        //Enable Notepad
        PlayerNotepad.OnNotepadEnabled += EnableNotepadUI;
        //Disable Notepad
        PlayerNotepad.OnNotepadDisabled += DisableAllUI;
        PlayerNotepad.OnNotepadDisabled += SetUIAccordingToGameState;


    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        //Enable Notepad
        PlayerNotepad.OnNotepadEnabled -= EnableNotepadUI;
        //Disable Notepad
        PlayerNotepad.OnNotepadDisabled -= DisableAllUI;
        PlayerNotepad.OnNotepadDisabled -= SetUIAccordingToGameState;
    }
}
