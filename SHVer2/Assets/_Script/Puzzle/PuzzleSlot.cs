using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector] public int puzzleSlotID;
    public RawImage rawImage;
    private PuzzlePiece piecePlaced;
    public bool pieceMatched;
    public static event Action CheckCompletion;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }
    public void OnDrop(PointerEventData eventData)
    {
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
