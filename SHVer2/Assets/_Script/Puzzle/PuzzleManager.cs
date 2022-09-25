using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleSlot> slotPrefabs;
    [SerializeField] private PuzzlePiece piecePrefab;
    [SerializeField] private Transform slotParent, pieceParent;
    [SerializeField] private Transform parentTransform;

    //Current Puzzle Slot references
    List<PuzzleSlot> currentSlots = new List<PuzzleSlot>();

    //Current Puzzle Slot references
    List<PuzzlePiece> currentPieces = new List<PuzzlePiece>();

    //Use to signal that the puzzle is complete
    public static event Action PuzzleIsComplete;

    //Random unique number
    List<int> PuzzlePiecePositionIndex = new List<int>();

    //Gets the number of quizzes the player answered
    public static Func<int> quizzesAnswered;
    private void Start()
    {
        RandomizePiecesPosition();
        Spawn();
        InitialPieces();
    }

    private void RandomizePiecesPosition()
    {
        List<int> values = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

        System.Random rand = new System.Random();

        PuzzlePiecePositionIndex = values.OrderBy(_ => rand.Next()).ToList();
    }

    private void InitialPieces()
    {
        for (int i = 4; i < 7; i++)
        {
            currentPieces[PuzzlePiecePositionIndex[i]].rawImage.enabled = true;
        }
    }

    private void EnablePuzzlePiece()
    {
        for (int i = 0; i < quizzesAnswered(); i++)
        {
            currentPieces[PuzzlePiecePositionIndex[i]].rawImage.enabled = true;
        }
    }

    void Spawn()
    {

        for (int i = 0; i < slotPrefabs.Count(); i++)
        {
            var spawnedSlot = Instantiate(slotPrefabs[i], slotParent.GetChild(i).position, Quaternion.identity, parentTransform);

            //Sets slot ID
            spawnedSlot.GetComponent<PuzzleSlot>().puzzleSlotID = i;

            //To put slot game objects to the very top.
            //Fixes overlap bug.
            spawnedSlot.transform.SetSiblingIndex(i);

            //Set current slot reference.
            currentSlots.Add(spawnedSlot.GetComponent<PuzzleSlot>());

            var spawnedPiece = Instantiate(piecePrefab, pieceParent.GetChild(PuzzlePiecePositionIndex[i]).position, Quaternion.identity, parentTransform);

            //Set current piece reference
            currentPieces.Add(spawnedPiece.GetComponent<PuzzlePiece>());

            spawnedPiece.GetComponent<PuzzlePiece>().Init(spawnedSlot.GetComponent<PuzzleSlot>());
        }
    }

    private void CheckCompletion()
    {
        bool isComplete = false;
        int numberOfMatchedPieces = 0;

        for (int i = 0; i < slotPrefabs.Count(); i++)
        {
            if (currentSlots[i].pieceMatched) numberOfMatchedPieces++;
            else if (!currentSlots[i].pieceMatched) numberOfMatchedPieces--;
            Debug.Log(numberOfMatchedPieces);
        }

        if (numberOfMatchedPieces == slotPrefabs.Count()) isComplete = true;

        if (isComplete)
        {
            Debug.Log("Puzzle Complete");
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            PuzzleIsComplete?.Invoke();
        }
        else
        {
            Debug.Log("Not Complete");
        }
    }

    private void OnEnable()
    {
        PuzzleSlot.CheckCompletion += CheckCompletion;

        PuzzlePiece.SendActivationMessage += EnablePuzzlePiece;

    }

    private void OnDisable()
    {
        PuzzleSlot.CheckCompletion -= CheckCompletion;

        PuzzlePiece.SendActivationMessage -= EnablePuzzlePiece;
    }
}
