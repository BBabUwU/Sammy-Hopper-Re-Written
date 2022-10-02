using UnityEngine;
using System;

[System.Serializable]
public class Quest
{
    public int questID;
    [HideInInspector] public bool completed;
    public string description;
    public Goal goal;
    public string Reward;

    public void Evaluate()
    {
        if (goal.IsReached())
        {
            completed = true;
        }
    }
}
