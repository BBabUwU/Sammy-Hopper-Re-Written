using UnityEngine;
using System;

[RequireComponent(typeof(PuzzlePiece))]
public class PuzzlePieceController : MonoBehaviour
{
    private PuzzlePiece puzzlePiece;
    public static event Action CheckActivation;
    private void Awake()
    {
        puzzlePiece = GetComponent<PuzzlePiece>();
    }

    private void EnablePiece(int id)
    {
        if (id == puzzlePiece.puzzlePieceID) puzzlePiece.rawImage.enabled = true;
    }

    private void OnEnable()
    {
        PuzzleManager.ActivatePiece += EnablePiece;
        CheckActivation?.Invoke();
    }

    private void OnDisable()
    {
        PuzzleManager.ActivatePiece -= EnablePiece;
    }
}
