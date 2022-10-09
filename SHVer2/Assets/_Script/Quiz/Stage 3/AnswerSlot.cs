using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSlot : MonoBehaviour, IDropHandler
{
    private ChoicePiece choice;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");

        if (eventData.pointerDrag != null)
        {
            choice = eventData.pointerDrag.GetComponent<ChoicePiece>();

            choice.isPlaced = true;

            //Snapping feature
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        if (choice.correctAnswer)
        {
            Debug.Log("Correct");
            Actions.correctGraph?.Invoke();
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
}
