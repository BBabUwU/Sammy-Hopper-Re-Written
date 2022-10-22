using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum CurrentAxis
{
    theX,
    theY
}

public enum LineTurn
{
    line_1,
    line_2
}

public class GraphManager : MonoBehaviour
{
    //NOTE! 0 = line 1 / 1 = line 2

    //Line to be currently drawn
    public LineTurn lineTurn;
    public CurrentAxis currentAxis;

    //Line prefab used
    public LineRenderer firstLine;
    public LineRenderer secondLine;

    //Line point a and point b
    private GameObject[] pos1 = new GameObject[2];
    private GameObject[] pos2 = new GameObject[2];

    //Instantiated line
    private GameObject line_1;
    private GameObject line_2;

    //Number of points placed
    private int[] points_placed = new int[2];

    //Current Answer
    private int[] currentAnswer = new int[2];

    //Check line
    //Check if both lines intersected
    private bool line1_intersected = false;
    private bool line2_intersected = false;
    private int[] firstInitialPoint = new int[2];
    private int[] secondInitialPoint = new int[2];

    private void Start()
    {
        Init();
    }

    public void Set_Line_1()
    {
        Reset_LineTurn_1();
    }

    public void Set_Line_2()
    {
        Reset_LineTurn_2();
    }

    public void Reset_LineTurn_1()
    {
        points_placed[0] = 0;
        lineTurn = LineTurn.line_1;
        currentAxis = CurrentAxis.theX;

        if (pos1[0] != null) pos1[0].GetComponent<PointController>().image.color = Color.clear;

        if (pos2[0] != null) pos2[0].GetComponent<PointController>().image.color = Color.clear;

        Reset_Values(0);
        Destroy(line_1);
    }

    public void Reset_LineTurn_2()
    {
        points_placed[1] = 0;
        lineTurn = LineTurn.line_2;
        currentAxis = CurrentAxis.theX;
        if (pos1[1] != null) pos1[1].GetComponent<PointController>().image.color = Color.clear;

        if (pos2[1] != null) pos2[1].GetComponent<PointController>().image.color = Color.clear;

        Reset_Values(1);
        Destroy(line_2);
    }

    private void Reset_Values(int i)
    {
        pos1[i] = null;
        pos2[i] = null;
    }

    private void Init()
    {
        firstLine.positionCount = 2;
        secondLine.positionCount = 2;
        lineTurn = LineTurn.line_1;
        points_placed[0] = 0;
        points_placed[1] = 1;
    }

    private void DrawLine()
    {
        if (lineTurn == LineTurn.line_1)
        {
            firstLine.SetPosition(0, pos1[0].transform.position);
            firstLine.SetPosition(1, pos2[0].transform.position);
            line_1 = Instantiate(firstLine).gameObject;
        }

        if (lineTurn == LineTurn.line_2)
        {
            secondLine.SetPosition(0, pos1[1].transform.position);
            secondLine.SetPosition(1, pos2[1].transform.position);
            line_2 = Instantiate(secondLine).gameObject;
        }
    }

    private void SetLine(int theXvalue, int theYvalue, GameObject obj)
    {
        int index = 0;

        //Sets what line we are on
        if (lineTurn == LineTurn.line_1)
        {
            if (points_placed[0] == 2) Reset_LineTurn_1();

            index = 0;
            obj.GetComponent<PointController>().image.color = Color.blue;
        }

        else if (lineTurn == LineTurn.line_2)
        {
            if (points_placed[1] == 2) Reset_LineTurn_2();

            index = 1;
            obj.GetComponent<PointController>().image.color = Color.red;
        }

        if (currentAxis == CurrentAxis.theX)
        {
            pos1[index] = obj;
            currentAxis = CurrentAxis.theY;
            points_placed[index]++;

            if (lineTurn == LineTurn.line_1)
            {
                firstInitialPoint[0] = theXvalue;
                firstInitialPoint[1] = theYvalue;
            }
            else if (lineTurn == LineTurn.line_2)
            {
                secondInitialPoint[0] = theXvalue;
                secondInitialPoint[1] = theYvalue;
            }
        }

        else if (currentAxis == CurrentAxis.theY)
        {
            pos2[index] = obj;
            if (pos1[index] != null && pos2[index] != null) DrawLine();
            points_placed[index]++;
            currentAxis = CurrentAxis.theX;
        }
    }

    public bool CheckGraph()
    {
        bool isCorrect = false;

        if (line1_intersected && line2_intersected)
        {
            if (Actions.currentQuiz().intialPoint_1_answer[0] == firstInitialPoint[0] && Actions.currentQuiz().intialPoint_1_answer[1] == firstInitialPoint[1])
            {
                if (Actions.currentQuiz().intialPoint_2_answer[0] == secondInitialPoint[0] && Actions.currentQuiz().intialPoint_2_answer[1] == secondInitialPoint[1])
                {
                    isCorrect = true;
                }
            }
        }

        return isCorrect;
    }

    private void Set_Intersect(int line)
    {
        if (line == 1) line1_intersected = true;
        else if (line == 2) line2_intersected = true;
    }

    private void Reset_Intersect(int line)
    {
        if (line == 1) line1_intersected = false;
        else if (line == 2) line2_intersected = false;
    }

    private int[] GetCurrentAnswer()
    {
        currentAnswer[0] = Int32.Parse(Actions.currentQuiz().questionAnswer1);
        currentAnswer[1] = Int32.Parse(Actions.currentQuiz().questionAnswer2);
        return currentAnswer;
    }

    private CurrentAxis GetLineTurn()
    {
        return currentAxis;
    }

    private void OnEnable()
    {
        Actions.CurrentAxis += GetLineTurn;
        Actions.GraphCurrentAnswer += GetCurrentAnswer;
        Actions.SetAxis += SetLine;
        Actions.LineIntersected += Set_Intersect;
        Actions.CheckGraph += CheckGraph;
        Actions.ResetLineIntersect += Reset_Intersect;
    }

    private void OnDisable()
    {
        Actions.CurrentAxis -= GetLineTurn;
        Actions.GraphCurrentAnswer -= GetCurrentAnswer;
        Actions.SetAxis -= SetLine;
        Actions.LineIntersected -= Set_Intersect;
        Actions.CheckGraph -= CheckGraph;
        Actions.ResetLineIntersect -= Reset_Intersect;
    }
}
