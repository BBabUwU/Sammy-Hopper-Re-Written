using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Collections;

public class ChoicePiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    public Image image;
    public bool isPlaced = false;
    public bool correctAnswer = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //So it can go through and land on the item slot.
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isPlaced = false;
        //Contains movement
        //Scalefactor is used to follow the mouse properly
        rectTransform.anchoredPosition += eventData.delta / Actions.canvas().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            transform.position = initialPosition;
        }

        canvasGroup.blocksRaycasts = true;
    }
}
