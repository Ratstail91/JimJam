using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	CircleCollider2D circleCollider;
	Rigidbody2D rigidBody;
	public float speed;
	public Vector2 direction;

	// Use this for initialization
	void Awake () {
		circleCollider = GetComponent<CircleCollider2D> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move() {
		rigidBody.velocity = Vector2.zero;
		rigidBody.AddForce (direction.normalized * speed, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.GetComponent<Fireball> () == null && collider.gameObject.GetComponent<Treasure> () == null) {
			Destroy (gameObject);
		}
	}
}
