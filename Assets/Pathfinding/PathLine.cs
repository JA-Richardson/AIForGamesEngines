using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathLine
{

    const float verticalGradient = 1e5f;
    float gradient;
    float y_intercept;
    Vector2 linePoint1;
    Vector2 linePoint2;

    float perpendicularGradient;
    bool approachSide;

    public PathLine(Vector2 linePoint, Vector2 linePerpendicular)
    {
        float dx = linePoint.x - linePerpendicular.x;
        float dy = linePoint.y - linePerpendicular.y;
        if(dx == 0)
        {
            perpendicularGradient = verticalGradient;
        }
        else 
        { 
            perpendicularGradient = dy / dx;
        }

        if (perpendicularGradient == 0)
        {
            gradient = verticalGradient;
        }
        else
        {
            gradient = -1 / perpendicularGradient;
        }

        y_intercept = linePoint.y - gradient * linePoint.x;
        linePoint1 = linePoint;
        linePoint2 = linePoint + new Vector2(1, gradient);
        approachSide = false;
        approachSide = GetSide(linePerpendicular);
    }
    
    bool GetSide(Vector2 p)
    {
        return (p.x - linePoint1.x) * (linePoint2.y - linePoint1.y) > (p.y - linePoint1.y) * (linePoint2.x - linePoint1.x);
    }

    public bool CrossedLine(Vector2 p)
    {
         return GetSide(p) != approachSide;
    }

    public void DrawWithGizmos(float length)
    {
        Vector3 lineDir = new Vector3(1, 0, gradient).normalized;
        Vector3 lineCenter = new Vector3(linePoint1.x, 0, linePoint1.y) + Vector3.up;
        Gizmos.DrawLine(lineCenter - lineDir * length / 2f, lineCenter + lineDir * length / 2f);
        
    }

    public float DistanceToEnd(Vector2 p)
    {
        float y_interceptPerpendicular = p.y - perpendicularGradient * p.x;
        float intersectX = (y_interceptPerpendicular - y_intercept) / (gradient - perpendicularGradient);
        float intersectY = gradient * intersectX + y_intercept;
        return Vector2.Distance(p, new Vector2(intersectX, intersectY));
    }

}
