using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathInspector : Editor {

    private Path path;
    private Path linkedPath;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 30;//determine curve resolution
    private const int velocitySteps = 10;
    private const int velocityShow = lineSteps / velocitySteps;
    private const float velocityScale = 0.2f;



    protected void OnSceneGUI()
    {
        path = target as Path;
        linkedPath = path.linkedPath;
        handleTransform = path.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3[] p = new Vector3[path.points.Length];
        for (int i = 0; i < p.Length; i++) { 
            p[i] = ShowPoint(i);
        }

        if (path.destination[0] != null) { 
            path.points[0] = path.transform.InverseTransformPoint(path.destination[0].transform.position);
        }
        if (path.destination[1] != null){ 
            path.points[path.points.Length - 1] = path.transform.InverseTransformPoint(path.destination[1].transform.position);
        }

        //draw control points
        Handles.color = Color.yellow;
        for (int i = 1; i < p.Length; i++) { 
            Handles.DrawLine(p[i - 1], p[i]);
        }

        //draw curve and velocity
        Vector3 lineStart = path.GetPoint(0);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + path.GetVelocity(0) * velocityScale);
        for (int i = 1; i <= lineSteps; i++) { 
            Vector3 lineEnd = path.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            if (i % velocityShow == 0) { 
                Handles.color = Color.green;
                Handles.DrawLine(lineEnd, lineEnd + path.GetVelocity(i / (float)lineSteps) * velocityScale);
            }
            lineStart = lineEnd;
        }
    }

    private Vector3 ShowPoint(int index) { 
        Vector3 point = handleTransform.TransformPoint(path.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck()) { 
            Undo.RecordObject(path, "Move Path Point");
            EditorUtility.SetDirty(path);
            path.points[index] = handleTransform.InverseTransformPoint(point);
            linkedPath.points[linkedPath.points.Length - 1 - index] = linkedPath.transform.InverseTransformPoint(point);
        }

        return point;
    }
}
