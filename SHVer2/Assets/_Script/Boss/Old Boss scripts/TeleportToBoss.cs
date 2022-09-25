using UnityEngine;
using System;

public class TeleportToBoss : MonoBehaviour
{

    [SerializeField] private Transform pointB;
    public static event System.Action Teleported;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = pointB.position;
            Teleported?.Invoke();
        }
    }
}
