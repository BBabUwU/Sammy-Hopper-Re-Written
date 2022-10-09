using UnityEngine;

public class startStg2Quiz : Interactable
{
    private bool hasStarted = false;
    private Stage2_Quiz quiz;
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        quiz = GetComponent<Stage2_Quiz>();
    }
    public override void Interact()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            col.enabled = false;
            quiz.enabled = true;
        }
    }
}
