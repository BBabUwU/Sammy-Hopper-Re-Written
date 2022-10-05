using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressManager : MonoBehaviour
{
    public List<QuestGiver> QuestList;

    //First part
    [SerializeField] private GameObject Blocked_area_1;
    private int answeredQuizzes = 0;
    [SerializeField] private GameObject Blocked_area_2;

    //Determines if the first part is completed
    private bool FirstPart;

    ///<Summary>
    ///Check part if complete
    ///</Summary>

    private void FirstPartComplete()
    {
        FirstPart = true;
        Destroy(Blocked_area_2);
    }


    ///<Summary>
    ///Puzzle Tracker
    ///</Summary>

    private int GetAnsweredQuiz()
    {
        return answeredQuizzes;
    }

    private void IncrementQuiz()
    {
        answeredQuizzes++;
    }

    ///<Summary>
    ///Blocked area functions
    ///</Summary>

    private void CheckClear()
    {
        Area_1_clear();
    }

    private void Area_1_clear()
    {
        bool objective_1 = false;
        bool objective_2 = false;

        if (QuestList != null)
            foreach (var item in QuestList)
            {
                if (item.quest.questID == 1 && item.quest.completed) objective_1 = true;
                if (item.quest.questID == 2 && item.quest.completed) objective_2 = true;
            }

        if (objective_1 && objective_2) Destroy(Blocked_area_1);
    }

    ///<Summary>
    ///Quest related functions
    ///</Summary>

    private void AddQuest(QuestGiver quest)
    {
        QuestList.Add(quest);
    }

    private List<QuestGiver> GetQuest()
    {
        return QuestList;
    }

    private void OnEnable()
    {
        ObjectiveManager.ListQuest += GetQuest;

        Tutorial.ListQuest += GetQuest;

        InteractionDialogue.addToList += AddQuest;

        Quest.CheckClear += CheckClear;

        PuzzleManager.quizzesAnswered += GetAnsweredQuiz;

        ExplorationQuiz.QuizComplete += IncrementQuiz;

        PuzzleManager.PuzzleIsComplete += FirstPartComplete;
    }

    private void OnDisable()
    {
        ObjectiveManager.ListQuest -= GetQuest;

        Tutorial.ListQuest -= GetQuest;

        InteractionDialogue.addToList -= AddQuest;

        Quest.CheckClear -= CheckClear;

        PuzzleManager.quizzesAnswered -= GetAnsweredQuiz;

        ExplorationQuiz.QuizComplete -= IncrementQuiz;

        PuzzleManager.PuzzleIsComplete -= FirstPartComplete;
    }
}
