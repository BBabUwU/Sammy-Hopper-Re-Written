public enum QuizType
{
    ExplorationQuiz,
    BossQuiz
}

[System.Serializable]
public class Quiz
{
    public int quizID;
    public QuizType quizType;
    public int score;
    public int totalScore;
    public bool isPassed;
}
