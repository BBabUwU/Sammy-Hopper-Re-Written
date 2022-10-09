using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage2QnATemplate
{
    public string question;
    public string answer;
    public List<string> choices;
    public bool notActive;
    public QuizDiff difficulty;
}
