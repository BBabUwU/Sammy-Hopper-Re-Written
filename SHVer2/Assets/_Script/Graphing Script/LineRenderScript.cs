using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class LineRenderScript : MonoBehaviour
{
    public int lineNumber;
    EdgeCollider2D edgeCollider;
    LineRenderer myLine;

    private void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        myLine = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SetEdgeCollider(myLine);
    }

    private void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponents<PointController>() != null)
        {
            PointController point = other.GetComponent<PointController>();

            if (point.xValue == Actions.GraphCurrentAnswer()[0] && point.yValue == Actions.GraphCurrentAnswer()[1])
            {
                Debug.Log("Intersect");
                Actions.LineIntersected?.Invoke(lineNumber);
            }
        }
    }

    private void OnDestroy()
    {
        Actions.ResetLineIntersect?.Invoke(lineNumber);
    }
}
