using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allowAnswer : MonoBehaviour
{
    private BoxCollider2D col;
    [SerializeField] private Stg2_Choices choice;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            choice.allowAnswer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            choice.allowAnswer = true;
        }
    }
}
