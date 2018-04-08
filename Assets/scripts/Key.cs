using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<Character> () != null) {
			GameObject.Find ("GamePlay").GetComponent<AudioPlayer> ().PlayCoinSound ();
			GameObject.Find ("GamePlay").GetComponent<GamePlay>().keys += 1;
			Destroy (gameObject);
		}
	}
}
