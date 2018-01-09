using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour {

    public const int NORTH = 0;
    public const int EAST = 1;
    public const int SOUTH = 2;
    public const int WEST = 3;

    public Path[] adjacencyNode = new Path[4];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
