using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour {
	public Text scoreText;
	public int score {
		get { return PersistentData.score; }
		set { PersistentData.score = value; }
	}
	public int keys;

	// Use this for initialization
	void Awake () {
		score = 0;
		keys = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;

		//check input
		if (Input.GetButtonDown("Quit")) {
			Quit ();
		}
	}

	public void Quit() {
		Application.Quit ();
	}

	public void GameOver() {
		SceneManager.LoadScene ("gameover");
	}
}
