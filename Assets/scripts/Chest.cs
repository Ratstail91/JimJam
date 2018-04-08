using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
	Treasure treasureScript;

	void Awake() {
		treasureScript = GetComponent<Treasure> ();
		treasureScript.enabled = false;
	}

	void Update() {
		if (GameObject.Find ("GamePlay").GetComponent<GamePlay> ().keys > 0) {
			treasureScript.enabled = true;
		} else {
			treasureScript.enabled = false;
		}
	}

	//NOTE: treasure script handles object destruction
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<Character> () != null && GameObject.Find ("GamePlay").GetComponent<GamePlay> ().keys > 0) {
			GameObject.Find ("GamePlay").GetComponent<GamePlay> ().keys -= 1;
		}
	}
}
