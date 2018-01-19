using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : BezierCurve {

    public GameNode[] destination = new GameNode[2];
    public Path linkedPath;

    public void Reset() {
        points = new Vector3[3] {
            new Vector3(0, 0),
            new Vector3(1, 0),
            new Vector3(2, 0)
        };
    }

    private GameNode[] prevDestination = new GameNode[2];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnValidate() { 
        checkLink();
    }

    private void checkLink() {
        if (destination[0] != null && destination[1] != null
            && (prevDestination[0] != destination[0]
                || prevDestination[1] != destination[1])) {
            for (int direction = 0; direction < 4; direction++) {
                if (destination[1].adjacencyNode[direction].destination[1] == destination[0]) {
                    linkedPath = destination[1].adjacencyNode[direction];
                    destination[1].adjacencyNode[direction].linkedPath = this;
                }
            }
        }
        
        prevDestination[0] = destination[0];
        prevDestination[1] = destination[1];

        if (linkedPath != null) {
            linkedPath.points = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++) {
                linkedPath.points[points.Length - 1 - i] = linkedPath.transform.InverseTransformPoint(
                                                           transform.TransformPoint(points[i]));
            }
        }
    }
}
