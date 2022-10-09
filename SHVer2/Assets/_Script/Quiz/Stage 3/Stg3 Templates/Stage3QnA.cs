using System.Collections.Generic;
using UnityEngine;


public enum LinearTopic
{
    Graphing,
    Elimination,
    Substitution,
    Determinants
}

[System.Serializable]
public class Stage3QnA
{
    public LinearTopic topic;
    public string question;
    public string answer1;
    public string answer2;
    public bool noSolution;
    public bool notActive;

    [Header("Only for graphing. Note: First answer")]
    public List<Sprite> choices;
}
