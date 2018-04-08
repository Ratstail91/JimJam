using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	public GameObject monsterPrefab;
	public GameObject targetObject;

	void Awake () {
		StartCoroutine (SpawnMonster ());
	}
	
	IEnumerator SpawnMonster() {
		while (true) {
			if (Vector3.Distance (transform.position, targetObject.transform.position) > 2.5f) {
				GameObject monster = Instantiate (monsterPrefab);
				monster.transform.position = transform.position;
				monster.GetComponent<Monster> ().targetObject = targetObject;
			}
			yield return new WaitForSeconds (10.0f);
		}
	}

	void OnBecameVisible() {
		enabled = false;
	}

	void OnBecameInvisible() {
		enabled = true;
	}
}
