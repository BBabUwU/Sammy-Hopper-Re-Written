using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject blockedArea;
    public int answeredQuizzes = 0;
    List<Quest> questList = new List<Quest>();
    public static event Action<int> VideoUnlocked;

    private void QuestComplete(Quest quest)
    {
        questList.Add(quest);
    }

    private void CheckUnlockedVideo()
    {
        int i = 0;
        foreach (var item in questList)
        {
            if (item.completed && item.VideoNumber == i)
            {
                VideoUnlocked?.Invoke(i);
            }
        }
    }

    private void QuizCompleted()
    {
        answeredQuizzes++;
    }

    private void PuzzleCompleted()
    {
        Destroy(blockedArea);
    }

    private int GetNumberOfAnsweredQuizzes()
    {
        return answeredQuizzes;
    }

    private void OnEnable()
    {
        PuzzleManager.PuzzleIsComplete += PuzzleCompleted;

        ExplorationQuiz.QuizComplete += QuizCompleted;

        PuzzleManager.quizzesAnswered += GetNumberOfAnsweredQuizzes;

        VideoMenuManager.MenuOpened += CheckUnlockedVideo;
    }

    private void OnDisable()
    {
        PuzzleManager.PuzzleIsComplete -= PuzzleCompleted;

        ExplorationQuiz.QuizComplete -= QuizCompleted;

        PuzzleManager.quizzesAnswered -= GetNumberOfAnsweredQuizzes;

        VideoMenuManager.MenuOpened -= CheckUnlockedVideo;
    }
}
