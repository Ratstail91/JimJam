using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	float startTime;
	public Text scoreText;

	// Use this for initialization
	void Awake () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Shoot") && Time.time - startTime > 2f) {
			SceneManager.LoadScene ("gameplay");
		}

		if (Input.GetButtonDown("Quit")) {
			Application.Quit ();
		}

		scoreText.text = "Score: " + PersistentData.score;
	}
}
