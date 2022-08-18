using UnityEngine;
using System;

public class GatherQuest : MonoBehaviour, IPickable
{
    [SerializeField] private QuestNumber questItem;
    public static Action<QuestNumber> itemCollected;

    private void OnDisable()
    {
        itemCollected?.Invoke(questItem);
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
