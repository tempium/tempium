using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier {

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        //return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);

        t = Mathf.Clamp01(t);
        float oneMinusT = 1 - t;
        return
                oneMinusT * oneMinusT * p0 +
                2 * oneMinusT * t * p1 +
                t * t * p2;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        t = Mathf.Clamp01(t);
        return
                2 * (1 - t) * (p1 - p0) +
                2 * t * (p2 - p1);
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);

        t = Mathf.Clamp01(t);
        float oneMinusT = 1 - t;
        return
                oneMinusT * oneMinusT * oneMinusT * p0 +
                3 * oneMinusT * oneMinusT * t * p1 +
                3 * oneMinusT * t * t * p2 +
                t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1 - t;
        return
                3 * oneMinusT * oneMinusT * (p1 - p0) +
                6 * oneMinusT * t * (p2 - p1) +
                3 * t * t * (p3 - p2);
    }

    public static Vector3 GetPoint(Vector3[] points, float t) {
        if (points.Length <= 4) {
            if (points.Length == 3) {
                return GetPoint(points[0], points[1], points[2], t);
            }
            if (points.Length == 4) {
                return GetPoint(points[0], points[1], points[2], points[3], t);
            }
            return new Vector3(0, 0, 0);
        }

        Vector3[] newPoints = new Vector3[points.Length - 1];
        for (int i = 0; i < newPoints.Length; i++) {
            newPoints[i] = Vector3.Lerp(points[i], points[i + 1], t);
        }

        return GetPoint(newPoints, t);
    }
}
