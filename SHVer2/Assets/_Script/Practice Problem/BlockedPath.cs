using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    [Header("Detection box size")]
    [SerializeField] private float boxWidth = 4f;
    [SerializeField] private float boxHeight = 6f;

    private QuizScript quizScript;

    private QuestManager questManager;

    private void Awake()
    {
        quizScript = GetComponent<QuizScript>();
        questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    private void Update()
    {
        PlayerDetected();
        QuizIsCompleted();
    }
    private Vector2 DetectionSize()
    {
        return new Vector2(boxWidth, boxHeight);
    }
    private void PlayerDetected()
    {
        RaycastHit2D playerDetected = Physics2D.BoxCast(this.transform.position, DetectionSize(), 0, Vector2.right, 0, LayerMask.GetMask("Player"));

        if (playerDetected.collider != null)
        {
            PlayerInputScript _playerInteraction = playerDetected.collider.GetComponent<PlayerInputScript>();

            if (_playerInteraction.InteractionButtonPressed())
            {
                quizScript.enabled = true;
            }
        }
    }

    private void QuizIsCompleted()
    {
        if (questManager.QuestIsCompleted(gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Display player detection
        Gizmos.DrawWireCube(this.transform.position, DetectionSize());
    }
}
