using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PointManager : MonoBehaviour
{
    public int startingXValue;
    public int yValue;
    List<PointController> points;

    private void Awake()
    {
        points = GetComponentsInChildren<PointController>(true).ToList();
    }

    private void Start()
    {
        foreach (var point in points)
        {
            point.xValue = this.startingXValue;
            point.yValue = this.yValue;
            startingXValue--;
        }
    }

}
