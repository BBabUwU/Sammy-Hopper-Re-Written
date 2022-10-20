using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isSet = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isSet)
            {
                isSet = true;
                Actions.SetCheckPoint?.Invoke(transform.position);
            }
        }
    }
}
