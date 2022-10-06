using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartablePuzzle : Interactable
{
    public UIType puzzleNumber;

    public override void Interact()
    {
        CanvasManager.Instance.SwitchCanvas(CanvasType.Puzzle);
        UIManager.Instance.TurnOnUI(puzzleNumber);
    }
}
