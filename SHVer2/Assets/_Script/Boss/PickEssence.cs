using UnityEngine;
using System;

public enum EssenceType
{
    easy,
    hard
}

public class PickEssence : MonoBehaviour, IPickable
{
    public EssenceType essenceType;
    public static event Action EssenseCount;
    public static event Action<EssenceType> essenceDifficulty;

    public void PickUp()
    {
        EssenseCount?.Invoke();
        essenceDifficulty?.Invoke(essenceType);
        DestroyEssence();
    }

    private void DestroyEssence()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        BossManager.destroyEssence += DestroyEssence;
    }

    private void OnDisable()
    {
        BossManager.destroyEssence -= DestroyEssence;
    }
}
