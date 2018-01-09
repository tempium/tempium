using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathInspector : BezierCurveInspector {

    private Path path;

    protected void OnSceneGUI() {
        base.OnSceneGUI();

        path = target as Path;

        if (path.destination[0] != null) {
            path.points[0] = path.transform.InverseTransformPoint(path.destination[0].transform.position);
        }
        if (path.destination[1] != null) { 
            path.points[path.points.Length - 1] = path.transform.InverseTransformPoint(path.destination[1].transform.position);
        }
    }
}
