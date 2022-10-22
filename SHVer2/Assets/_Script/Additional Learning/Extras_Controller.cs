using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Extras_Controller : MonoBehaviour
{
    public int extrasID;
    public Sprite extra;
    private Button button;
    [SerializeField] private GameObject viewWindow;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(On_Click);

    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        button.interactable = false;

        if (Actions.ExtrasList != null)
        {
            foreach (var extraIndex in Actions.ExtrasList())
            {
                if (extraIndex == extrasID)
                {
                    button.interactable = true;
                }
            }
        }
    }

    private void On_Click()
    {
        viewWindow.SetActive(true);
        viewWindow.GetComponent<Image>().sprite = extra;
    }
}
