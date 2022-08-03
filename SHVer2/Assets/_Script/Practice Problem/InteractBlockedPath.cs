using UnityEngine;

public class InteractBlockedPath : MonoBehaviour
{
    [SerializeField] private float boxWidth = 4f;
    [SerializeField] private float boxHeight = 6f;

    private QuizScript _quizScript;

    private void Awake()
    {
        _quizScript = GetComponent<QuizScript>();
    }

    private void Update()
    {
        PlayerDetected();
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
                _quizScript.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Display player detection
        Gizmos.DrawWireCube(this.transform.position, DetectionSize());
    }
}
