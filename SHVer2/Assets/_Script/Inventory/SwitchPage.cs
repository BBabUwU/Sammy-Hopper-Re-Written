using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SwitchPage : MonoBehaviour
{
    public List<GameObject> pages;
    public Button nextPage;
    public Button previousPage;
    private int currentPage = 0;
    private void Awake()
    {
        nextPage.onClick.AddListener(NextPage);
        previousPage.onClick.AddListener(PreviousPage);
    }

    private void NextPage()
    {
        currentPage++;

        if (currentPage != pages.Count)
        {
            pages.ForEach(x => x.gameObject.SetActive(false));
            pages[currentPage].SetActive(true);
        }
        else
        {
            currentPage--;
        }
    }

    private void PreviousPage()
    {
        currentPage--;

        if (currentPage >= 0)
        {
            pages.ForEach(x => x.gameObject.SetActive(false));
            pages[currentPage].SetActive(true);
        }
        else
        {
            currentPage++;
        }
    }
}
