using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpawn : MonoBehaviour {

	public GameNode spawnNode;

	void Start () {
		transform.position = spawnNode.transform.position;
	}

}
