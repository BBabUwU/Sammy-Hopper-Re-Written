using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectiveManager : MonoBehaviour
{
    List<ObjectiveController> objectivePanels;

    private void Awake()
    {
        objectivePanels = GetComponentsInChildren<ObjectiveController>(true).ToList();
    }

    private void OnEnable()
    {
        if (Actions.ListQuest != null) AddObjective();
    }

    private void AddObjective()
    {
        int i = 0;

        foreach (var quest in Actions.ListQuest())
        {
            if (quest == null)
            {
                Debug.Log("Null");
                i++;
            }
            else
            {

                foreach (var item in objectivePanels)
                {
                    if (item.objectiveNumber == quest.quest.questID)
                    {
                        item.questGiver = quest;
                        item.gameObject.SetActive(true);
                        i++;
                    }
                }
            }
        }

    }
}