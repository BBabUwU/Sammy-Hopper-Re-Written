using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheck : MonoBehaviour
{
    [SerializeField] private int puzzleNumber;

    private void CheckComplete(int puzzleNumber)
    {
        if (this.puzzleNumber == puzzleNumber) Destroy(gameObject);
    }

    private void OnEnable()
    {
        PuzzleManager.PuzzleIsComplete += CheckComplete;
    }

    private void OnDisable()
    {
        PuzzleManager.PuzzleIsComplete -= CheckComplete;
    }
}
