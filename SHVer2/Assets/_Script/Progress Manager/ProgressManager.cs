using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject blockedArea;

    private void PuzzleCompleted()
    {
        Destroy(blockedArea);
    }

    private void OnEnable()
    {
        PuzzleManager.PuzzleIsComplete += PuzzleCompleted;
    }

    private void OnDisable()
    {
        PuzzleManager.PuzzleIsComplete -= PuzzleCompleted;
    }
}
