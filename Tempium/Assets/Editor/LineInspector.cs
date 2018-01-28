using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor {

    void OnSceneGUI() {
        //target is the current selected GameObject
        Line line = target as Line;

        Transform handleTransform = line.transform;
        //check for pivot mode
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        //line position editor gui
        EditorGUI.BeginChangeCheck();
        p0 = Handles.DoPositionHandle(p0, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            //record change to make undo works
            Undo.RecordObject(line, "Move Point");
            //tell unity that line has been changed
            EditorUtility.SetDirty(line);
            line.p0 = handleTransform.InverseTransformPoint(p0);
        }

        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p1 = handleTransform.InverseTransformPoint(p1);
        }

        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);
    }
}
