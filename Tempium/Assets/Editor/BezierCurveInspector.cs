using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve), true)]
public class BezierCurveInspector : Editor {

    private BezierCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 30;//determine curve resolution
    private const int velocitySteps = 10;
    private const int velocityShow = lineSteps / velocitySteps;
    private const float velocityScale = 0.2f;

    

    protected void OnSceneGUI() {
        curve = target as BezierCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3[] p = new Vector3[curve.points.Length];
        for (int i = 0; i < p.Length; i++) {
            p[i] = ShowPoint(i);
        }

        //draw control points
        Handles.color = Color.yellow;
        for (int i = 1; i < p.Length; i++) { 
            Handles.DrawLine(p[i - 1], p[i]);
        }

        //draw curve and velocity
        Vector3 lineStart = curve.GetPoint(0);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + curve.GetVelocity(0) * velocityScale);
        for (int i = 1; i <= lineSteps; i++) {
            Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            if (i % velocityShow == 0) {
                Handles.color = Color.green;
                Handles.DrawLine(lineEnd, lineEnd + curve.GetVelocity(i / (float)lineSteps) * velocityScale);
            }
            lineStart = lineEnd;
        }
    }

    private Vector3 ShowPoint(int index) {
        Vector3 point = handleTransform.TransformPoint(curve.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(curve, "Move Bezier Curve Point");
            EditorUtility.SetDirty(curve);
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }

        return point;
    }
}
