using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySlain : MonoBehaviour
{
    private void OnDestroy()
    {
        Actions.decreaseEnemyCounter?.Invoke();
    }
}
