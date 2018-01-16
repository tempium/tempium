using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public Path path;
	public GameNode node;
	public GameNode startNode;
	// Use this for initialization
	private Quaternion rotation;
	private Quaternion destRotation;
	private float n = 0.5f;
	private Vector3 temp;
	private Quaternion temp1;
	private Quaternion startTemp;
	private enum stateRotate {left,right};
	private stateRotate myState;
	private int currentDirection = GameNode.NORTH;
	void Start () {
		myState = stateRotate.right;
		node = startNode;
		transform.position = node.transform.position;

		temp = transform.position;
		temp.y += 1;
		
		transform.position = temp; 
		startTemp = transform.rotation;

//		transform.rotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));
//		rotation = transform.rotation;
		path = node.adjacencyNode[currentDirection];
	}
	
	// Update is called once per frame
	void Update () {
		if(myState == stateRotate.right) {rotateRight ();}
		else if(myState == stateRotate.left) {rotateLeft ();}
	}

	void rotateRight () {
//		temp1 = transform.rotation;
//		print (transform.rotation);
//		temp1.y += 0.03f;
//		transform.rotation = temp1;
//		if (transform.rotation.y > 0.990f) {
//			myState = stateRotate.left;
//		}
		print(transform.rotation);
		transform.Rotate(Vector3.up*2 , Space.World);
	}

	void rotateLeft () {
		temp1 = transform.rotation;
		print (transform.rotation);
		temp1.y -= 0.03f;
		transform.rotation = temp1;
		if (transform.rotation.y < 0.011f) {
			myState = stateRotate.right;
		}
	}
}
