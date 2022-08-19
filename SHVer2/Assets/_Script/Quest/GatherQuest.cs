using UnityEngine;
using System;

public class GatherQuest : MonoBehaviour, IPickable
{
    [SerializeField] private int questItemID;
    public static Action<int> itemCollected;

    private void OnDisable()
    {
        itemCollected?.Invoke(questItemID);
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
