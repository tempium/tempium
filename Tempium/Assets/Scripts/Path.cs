using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : BezierCurve {

    public GameNode[] destination = new GameNode[2];

    public void Reset() {
        points = new Vector3[3] {
            new Vector3(0, 0),
            new Vector3(1, 0),
            new Vector3(2, 0)
        };
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
