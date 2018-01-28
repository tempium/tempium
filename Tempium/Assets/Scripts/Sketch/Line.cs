using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public Vector3 p0, p1;
    public bool showGizmos = true;

    //private LineRenderer lineRenderer;

    private void Awake() {
        //lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.TransformPoint(p0), transform.TransformPoint(p1));
    }
}
