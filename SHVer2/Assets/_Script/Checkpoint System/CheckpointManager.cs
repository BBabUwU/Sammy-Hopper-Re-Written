using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector2 lastCheckPointPos;

    private void SetCheckPoint(Vector2 player)
    {
        lastCheckPointPos = player;
    }

    private void OnEnable()
    {
        Actions.SetCheckPoint += SetCheckPoint;
    }

    private void OnDisable()
    {
        Actions.SetCheckPoint -= SetCheckPoint;
    }
}
