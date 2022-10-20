using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    private Vector2 lastCheckPointPos;
    [SerializeField] private Transform player;
    [SerializeField] private int lives;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Awake()
    {
        button.onClick.AddListener(On_Click);
    }

    private void On_Click()
    {
        if (lives == 0)
        {
            Reset_Level();
        }
        else
        {
            LoadLastCheckpoint();
        }
    }

    private void LoadLastCheckpoint()
    {
        player.position = lastCheckPointPos;
        Actions.setAllControls(true);
        Actions.SetPlayerHealth?.Invoke(100f);
        Actions.ResetDeath?.Invoke(false);
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        UIManager.Instance.TurnOffUI(UIType.PlayerDeathUI);
        --lives;
    }

    private void Reset_Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Check_Lives()
    {
        if (lives == 0)
        {
            buttonText.text = "Restart Level";
            livesText.text = "x " + lives;
        }
        else
        {
            buttonText.text = "Reload last checkpoint";
            livesText.text = "x " + lives;
        }
    }

    private void SetCheckPoint(Vector2 player)
    {
        lastCheckPointPos = player;
    }

    private void OnEnable()
    {
        Actions.SetCheckPoint += SetCheckPoint;
        Actions.Check_Lives += Check_Lives;
    }

    private void OnDisable()
    {
        Actions.SetCheckPoint -= SetCheckPoint;
        Actions.Check_Lives -= Check_Lives;
    }
}
