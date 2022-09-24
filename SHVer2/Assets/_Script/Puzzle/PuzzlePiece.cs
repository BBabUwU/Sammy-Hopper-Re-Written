using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Canvas is referenced to access the scale of the canvas.
    public static event Func<Canvas> canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PuzzleSlot slot;
    private RawImage rawImage;

    //Puzzle Index
    [HideInInspector] public int puzzlePieceID;

    //Original position
    private Vector3 originalPosition;

    public void Init(PuzzleSlot slot)
    {
        originalPosition = transform.position;
        puzzlePieceID = slot.puzzleSlotID;
        rawImage.texture = slot.rawImage.texture;
        this.slot = slot;
        //gameObject.SetActive(false);
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rawImage = GetComponent<RawImage>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //So it can go through and land on the item slot.
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Contains movement
        //Scalefactor is used to follow the mouse properly
        rectTransform.anchoredPosition += eventData.delta / canvas().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}
