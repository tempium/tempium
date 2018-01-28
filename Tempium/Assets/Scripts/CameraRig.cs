﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = player.transform.position-transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position+ offset;
	}
}
