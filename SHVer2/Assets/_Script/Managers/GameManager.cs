using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Makes one instance of the gamemanager and can be accessed by other scripts.
    public static GameManager Instance; //Singleton

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        //Checks if this instance has been set.
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
