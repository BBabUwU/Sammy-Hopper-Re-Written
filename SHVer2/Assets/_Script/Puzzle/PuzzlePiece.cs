using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Canvas is referenced to access the scale of the canvas.
    public static event Func<Canvas> canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PuzzleSlot slot;
    public RawImage rawImage;

    //Puzzle Index
    [HideInInspector] public int puzzlePieceID;

    //Original position
    private Vector3 originalPosition;

    //Sends message for activation
    public static event Action SendActivationMessage;

    public void Init(PuzzleSlot slot)
    {
        originalPosition = transform.position;
        puzzlePieceID = slot.puzzleSlotID;
        rawImage.texture = slot.rawImage.texture;
        rectTransform.sizeDelta = slot.rect.sizeDelta;
        this.slot = slot;
        rawImage.enabled = false;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rawImage = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitActivation());
    }

    IEnumerator WaitActivation()
    {
        yield return new WaitForSeconds(0.1f);
        SendActivationMessage?.Invoke();
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
