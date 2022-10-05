using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [HideInInspector] public Tutorial currentTutorial;
    private Image imageTexture;
    [SerializeField] private Button nextSlide;
    [SerializeField] private Button previousSlide;
    [SerializeField] private Button close;
    private int currentPage = 0;

    private void Awake()
    {
        imageTexture = GetComponent<Image>();
        nextSlide.onClick.AddListener(NextSlide);
        previousSlide.onClick.AddListener(PreviousSlide);
        close.onClick.AddListener(ReturnTutorialSelect);
    }
    private void OnEnable()
    {
        imageTexture.sprite = currentTutorial.images[currentPage];
    }

    private void NextSlide()
    {
        currentPage++;

        if (currentPage != currentTutorial.images.Count)
        {
            imageTexture.sprite = currentTutorial.images[currentPage];
        }
        else
        {
            currentPage--;
            Debug.Log("The last slide");
        }
    }

    private void PreviousSlide()
    {
        currentPage--;

        if (currentPage >= 0)
        {
            imageTexture.sprite = currentTutorial.images[currentPage];
        }
        else
        {
            currentPage++;
            Debug.Log("The first slide");
        }
    }

    private void ReturnTutorialSelect()
    {
        currentPage = 0;
        imageTexture.sprite = currentTutorial.images[0];
        UIManager.Instance.TurnOffUI(UIType.TutorialWindow);
    }
}
