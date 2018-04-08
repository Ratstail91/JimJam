using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject playerObject;
	Vector3 offset;

	// Use this for initialization
	void Awake () {
		offset = transform.position - playerObject.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (playerObject != null) {
			transform.position = playerObject.transform.position + offset;
		}
	}
}
