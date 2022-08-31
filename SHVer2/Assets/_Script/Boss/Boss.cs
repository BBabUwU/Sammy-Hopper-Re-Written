using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator bossAnimator;
    private QuizScript bossQuiz;
    public Transform player;
    public bool isFlipped = false;

    private void Awake()
    {
        bossQuiz = GetComponent<QuizScript>();
    }

    private void StartQuiz()
    {
        if (bossQuiz != null)
        {
            bossQuiz.enabled = true;
        }
        else { Debug.Log("Quiz Script not found"); }
    }

    private QuizScript GetQuiz() { return bossQuiz; }

    private void StartBossFight()
    {

    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= 1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void OnEnable()
    {
        BossManager.StartPhase_1 += StartQuiz;
        BossManager.BossQuizScript += GetQuiz;
    }

    private void OnDisable()
    {
        BossManager.StartPhase_1 -= StartQuiz;
        BossManager.BossQuizScript -= GetQuiz;
    }
}
