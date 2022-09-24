using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartablePuzzle : Interactable
{
    public override void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
    }
}
