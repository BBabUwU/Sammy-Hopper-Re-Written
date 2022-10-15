using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Choice { A, B, C, D }
public class Stg2_Choices : Interactable
{
    public Choice choice;
    public int quizNumber;
    private string choiceAnswer;
    private BoxCollider2D interactCol;
    private BoxCollider2D col;
    public bool allowAnswer;
    public Transform teleportPoint;
    public Transform playerTransform;
    private void Awake()
    {
        allowAnswer = true;
        col = transform.GetChild(0).GetComponent<BoxCollider2D>();
        interactCol = GetComponent<BoxCollider2D>();
        interactCol.enabled = false;
    }

    public override void Interact()
    {
        if (allowAnswer)
            SelectAnswer();
    }

    private void SetValue(Choice choice, string answer)
    {
        if (this.choice == choice) choiceAnswer = answer;
    }

    private void SelectAnswer()
    {
        Actions.answer?.Invoke(choiceAnswer);
    }

    private void Punish(string answer, int quizNum)
    {
        if (choiceAnswer == answer && quizNum == quizNumber)
        {
            col.enabled = false;
            StartCoroutine(enableCollision());
        }
    }

    IEnumerator enableCollision()
    {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
        Actions.setMovement(false);
        playerTransform.position = teleportPoint.position;
        Debug.Log(allowAnswer);
        StartCoroutine(TempStopMovement());
    }

    IEnumerator TempStopMovement()
    {
        yield return new WaitForSeconds(0.5f);
        Actions.setMovement(true);
    }

    private void OnEnable()
    {
        Actions.updateChoiceText += SetValue;
        Actions.punish += Punish;
    }

    private void OnDisable()
    {
        Actions.updateChoiceText -= SetValue;
        Actions.punish -= Punish;
    }
}
