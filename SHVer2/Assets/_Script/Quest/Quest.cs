using UnityEngine;

[System.Serializable]
public class Quest
{
    public int questNumber;
    [HideInInspector] public bool completed;
    public string title;
    public string description;
    public Goal goal;

    public void Evaluate()
    {
        if (goal.IsReached())
        {
            completed = true;
        }
    }
}
