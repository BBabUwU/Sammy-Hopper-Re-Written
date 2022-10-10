using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            Destroy(other.gameObject);
            Actions.meteorAtkOver?.Invoke();
        }
    }
}
