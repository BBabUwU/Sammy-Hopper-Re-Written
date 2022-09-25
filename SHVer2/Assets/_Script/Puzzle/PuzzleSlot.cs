using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector] public int puzzleSlotID;
    public RawImage rawImage;
    public RectTransform rect;
    private PuzzlePiece piecePlaced;
    public bool pieceMatched;
    public static event Action CheckCompletion;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");

        if (eventData.pointerDrag != null)
        {
            piecePlaced = eventData.pointerDrag.GetComponent<PuzzlePiece>();

            //Snapping feature
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Check();
        }
    }

    private void Check()
    {
        if (piecePlaced.puzzlePieceID == puzzleSlotID)
        {
            pieceMatched = true;
        }
        else
        {
            pieceMatched = false;
        }

        CheckCompletion?.Invoke();
    }
}
