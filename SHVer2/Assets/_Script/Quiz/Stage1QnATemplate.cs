public enum QuizDiff
{
    Easy,
    Hard
}

[System.Serializable]

public class Stage1QnATemplate
{
    public string question;
    public string correctAnswer1;
    public string correctAnswer2;
    public bool NotActive;
    public QuizDiff difficulty;
}
