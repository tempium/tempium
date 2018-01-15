//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//
//using UnityEngine;
//
//public class CoinPos : MonoBehaviour {
//	public Path path;
//	public GameNode node;
//	public GameNode startNode;
//	// Use this for initialization
//
//	private int currentDirection = GameNode.NORTH;
//	void Start () {
//		node = startNode;
//
//		transform.position = node.transform.position;
//		transform.rotation = Quaternion.LookRotation(node.adjacencyNode[currentDirection].GetDirection(0));
////		rotation = transform.rotation;
//		path = node.adjacencyNode[currentDirection];
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}
