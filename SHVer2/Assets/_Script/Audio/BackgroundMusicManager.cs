using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        AudioManager.Instance.StopAll();
        if (buildIndex == 0)
        {
            AudioManager.Instance.Play("main_menu");
        }

        else if (buildIndex == 1)
        {
            AudioManager.Instance.Play("stg1_bgmusic");
        }

        else if (buildIndex == 2)
        {
            AudioManager.Instance.Play("cutscene_1");
        }

        else if (buildIndex == 3 || buildIndex == 4)
        {
            AudioManager.Instance.Play("stg1_bgmusic");
        }

        else if (buildIndex == 5)
        {
            AudioManager.Instance.Play("cutscene_2");
        }

        else if (buildIndex == 6 || buildIndex == 7)
        {
            AudioManager.Instance.Play("stg2_bgmusic");
        }

        else if (buildIndex == 8)
        {
            AudioManager.Instance.Play("cutscene_3");
        }

        else if (buildIndex == 9 || buildIndex == 10 || buildIndex == 11)
        {
            AudioManager.Instance.Play("stg3_bgmusic");
        }

        else if (buildIndex == 12)
        {
            AudioManager.Instance.Play("cutscene_ending");
        }
    }
}
