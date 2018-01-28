﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {

    public Vector3[] points;

    public void Reset() {
        points = new Vector3[] {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0),
            new Vector3(3, 0, 0),
            new Vector3(4, 0, 0)
        };
    }

    public Vector3 GetPoint(float t) {
        //return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], t));
        return transform.TransformPoint(Bezier.GetPoint(points, t));
    }

    public Vector3 GetVelocity(float t) {
        if (points.Length == 3) {
            return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], t)) - transform.position;
        }
        if (points.Length == 4) {
            return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
        }
        return new Vector3(0, 0, 0);
    }

    public Vector3 GetDirection(float t) {
        if (points.Length == 3 || points.Length == 4) {
            return GetVelocity(t).normalized;
        }
        return new Vector3(1, 0, 0);
    }
}
