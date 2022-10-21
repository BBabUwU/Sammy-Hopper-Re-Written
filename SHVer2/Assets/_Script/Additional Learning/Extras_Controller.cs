using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Extras_Controller : MonoBehaviour
{
    private Image image;
    public int extrasID;
    public Sprite thumbnail;
    public Sprite extra;
    private Button button;
    [SerializeField] private GameObject viewWindow;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(On_Click);

        image.sprite = thumbnail;
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        image.enabled = false;

        if (Actions.ExtrasList != null)
        {
            foreach (var extraIndex in Actions.ExtrasList())
            {
                if (extraIndex == extrasID)
                {
                    image.enabled = true;
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
