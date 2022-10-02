using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Bookmark
{
    ObjectivesBookmark,
    TutorialBookmark
}

public class InvPageSwitcher : MonoBehaviour
{
    public Bookmark bookmark;
    public Transform bookmarkSelect;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchPage);
    }

    private void Start()
    {
        //Initial page
        UIManager.Instance.TurnOnUI(UIType.ObjectivePage);
    }

    private void SwitchPage()
    {

        bookmarkSelect.position = transform.position;

        if (bookmark == Bookmark.ObjectivesBookmark)
        {
            //Puts the bookmark in this button
            UIManager.Instance.TurnOffUI(UIType.TutorialPage);
            UIManager.Instance.TurnOnUI(UIType.ObjectivePage);
        }
        else if (bookmark == Bookmark.TutorialBookmark)
        {
            //Puts the bookmark in this button
            UIManager.Instance.TurnOnUI(UIType.TutorialPage);
            UIManager.Instance.TurnOffUI(UIType.ObjectivePage);
        }
    }
}
