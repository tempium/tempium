using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	
	public GameNode node;
	public GameNode startNode;
	// Use this for initialization
	private bool isPick;
	private Quaternion rotation;
	private Quaternion destRotation;
	private float n = 0.5f;
	private Vector3 temp;
	private enum stateRotate {left,right};
	private stateRotate myState;
	private int currentDirection = GameNode.NORTH;

	void Start () {
		coinPosStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if(myState == stateRotate.right) {rotateRight ();}
	}

	void rotateRight () {
		print(transform.rotation);
		transform.Rotate(Vector3.up*2 , Space.World);
	}

	void coinPosStart(){
		myState = stateRotate.right;
		node = startNode;
		transform.position = node.transform.position;

		temp = transform.position;
		temp.y += 1;

		transform.position = temp; 
		isPick = false;
	}

	bool wasPicked() {
		return isPick;
	}

}
