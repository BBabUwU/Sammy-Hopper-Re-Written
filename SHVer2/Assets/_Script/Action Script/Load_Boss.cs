using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Boss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(2);
    }
}
