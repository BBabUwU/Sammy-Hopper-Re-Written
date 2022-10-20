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
    public string question_1;
    public string question_2;
    public string answer1;
    public string answer2;
    public bool noSolution;
    public bool notActive;

    [Header("For Graphing only")]
    public int[] first_initialPoint;
    public int[] second_initialPoint;

}
