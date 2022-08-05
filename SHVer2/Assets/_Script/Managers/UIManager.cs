using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _notepadUI;
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _quizUI;

    private void Update()
    {
        IsUsingNotepad();
    }

    private void IsUsingNotepad()
    {
        if (GameManager.Instance.isUsingNotepad)
        {
            DisableAllUI();
            _notepadUI.SetActive(true);
        }
        else if (GameManager.Instance.gameState == GameState.AnsweringQuiz)
        {
            //If the player disable the camera while answering a quiz, it will re-enable quiz UI.
            _notepadUI.SetActive(false);
            _quizUI.SetActive(true);
        }
        else
        {
            //If player is not answering quiz, it will simply just disable the notepadUI
            _notepadUI.SetActive(false);
        }
    }

    private void DisableAllUI()
    {
        _notepadUI.SetActive(false);
        _playerUI.SetActive(false);
        _quizUI.SetActive(false);
    }

    //Event functions
    //Listening to game manager.
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _quizUI.SetActive(state == GameState.AnsweringQuiz);
    }
}
