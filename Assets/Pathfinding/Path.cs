using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookPoints;
    public readonly PathLine[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;


    public Path(Vector3[] waypoint, Vector3 startPos, float turnDist, float stoppingDist)
    {
        lookPoints = waypoint;
        turnBoundaries = new PathLine[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 prevPoint = V3ToV2(startPos);
        for(int i= 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - prevPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDist;
            turnBoundaries[i] = new PathLine(turnBoundaryPoint, prevPoint - dirToCurrentPoint * turnDist);
            prevPoint = turnBoundaryPoint;
        }

        float distFromEnd = 0;
        for (int i = lookPoints.Length - 1; i > 0; i--)
        {
            distFromEnd += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if (distFromEnd > stoppingDist)
            {
                slowDownIndex = i;
                break;
            }
        }
    }

    Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector3 p in lookPoints)
        {
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }
        Gizmos.color = Color.white;
        foreach (PathLine p in turnBoundaries)
        {
            p.DrawWithGizmos(10);
        }
        
    }
        
}
