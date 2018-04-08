using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
	public int value;

	void OnTriggerEnter2D(Collider2D collider) {
		if (this.enabled && collider.gameObject.GetComponent<Character> () != null) {
			GameObject.Find ("GamePlay").GetComponent<GamePlay> ().score += value;
			Destroy (gameObject);
			GameObject.Find("GamePlay").GetComponent<AudioPlayer>().PlayCoinSound();
		}
	}
}
