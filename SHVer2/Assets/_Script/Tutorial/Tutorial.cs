using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public List<Sprite> images;
    public int pageID;
    public bool isUnlocked = false;
    private Button button;
    public TutorialController tutorialSlider;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        if (isUnlocked)
        {
            Debug.Log("Here");
            tutorialSlider.currentTutorial = this;
            UIManager.Instance.TurnOnUI(UIType.TutorialWindow);
        }
    }
}
