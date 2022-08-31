using UnityEngine;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
    List<Quiz> quizRecord = new List<Quiz>();
    List<Quest> questRecord = new List<Quest>();

    private void AddQuiz(Quiz quiz)
    {
        quizRecord.Add(quiz);
    }

    private void AddQuest(Quest quest)
    {
        questRecord.Add(quest);
    }

    private void UpdateQuestRecord(Quest quest)
    {
        foreach (Quest questList in questRecord)
        {
            if (questList.questID == quest.questID)
            {
                questList.completed = quest.completed;
                questList.goal = quest.goal;
            }
        }
    }

    private void UpdateQuizRecord(Quiz quiz)
    {
        foreach (Quiz quizList in quizRecord)
        {
            if (quiz.quizID == quizList.quizID)
            {
                quizList.isPassed = quiz.isPassed;
                quizList.score = quiz.score;
            }
        }
    }

    private bool CheckQuizIfPassed(int quizID)
    {
        bool quizPassed = false;

        foreach (Quiz recordedQuiz in quizRecord)
        {
            if (recordedQuiz.quizID == quizID && recordedQuiz.isPassed)
            {
                quizPassed = true;
            }
        }

        return quizPassed;
    }

    private bool CheckQuestIfPassed(int questNumber)
    {
        bool questPassed = false;

        foreach (Quest recordedQuest in questRecord)
        {
            if (recordedQuest.questID == questNumber && recordedQuest.completed)
            {
                questPassed = true;
            }
        }

        return questPassed;
    }

    private void OnEnable()
    {
        QuizScript.QuizPassed += UpdateQuizRecord;
        QuizScript.AddQuiz += AddQuiz;

        QuestGiver.QuestPassed -= UpdateQuestRecord;
        QuestGiver.AddQuest += AddQuest;
    }

    private void OnDisable()
    {
        QuizScript.QuizPassed -= UpdateQuizRecord;
        QuizScript.AddQuiz -= AddQuiz;

        QuestGiver.QuestPassed -= UpdateQuestRecord;
        QuestGiver.AddQuest -= AddQuest;
    }

}
