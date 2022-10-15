using UnityEngine;

public class HitTaken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Actions.addEveluation?.Invoke("hit");
    }
}
