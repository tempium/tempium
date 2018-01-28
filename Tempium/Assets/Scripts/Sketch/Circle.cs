using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    public float radius = 1;
    public int vertexCount = 40;
    public bool showGizmos = true;

    //private LineRenderer lineRenderer;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake() {
        //lineRenderer = GetComponent<LineRenderer>();
    }

    public void Reset() {
        radius = 1;
        vertexCount = 40;
    }

    private void OnDrawGizmos() {
        if (!showGizmos) {
            return;
        }

        float theta = 0;
        startPoint.Set(radius, 0, 0);

        for (int i = 1; i <= vertexCount; i++) {
            theta = (2 * Mathf.PI) * i / vertexCount;
            endPoint.Set(radius * Mathf.Cos(theta), 0, radius * Mathf.Sin(theta));

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.TransformPoint(startPoint), transform.TransformPoint(endPoint));

            startPoint = endPoint;
        }
    }
}
