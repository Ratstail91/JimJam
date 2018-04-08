using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	Animator animator;
	BoxCollider2D boxCollider;
	Durability durability;
	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	Unit2D unit2D;

	Vector2 lastDirection;
	public GameObject targetObject;

	public GameObject fireballPrefab;
	public GameObject treasurePrefab;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		durability = GetComponent<Durability> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		unit2D = GetComponent <Unit2D> ();

		unit2D.speed = 0.4f;

		durability.maxHealthPoints = 3;
		durability.healthPoints = 3;

		durability.onDamaged = (int diff) => {
			GameObject.Find("GamePlay").GetComponent<AudioPlayer>().PlayHurtSound();
			FlashColor(0.5f, 0, 0, 0.1f);
		};
		durability.onHealed = (int diff) => {
			FlashColor(0, 1, 0, 0.1f);
		};
		durability.onDestruction = (int diff) => {
			GameObject.Find("GamePlay").GetComponent<AudioPlayer>().PlayKillSound();
			GameObject treasure = Instantiate(treasurePrefab);
			treasure.transform.position = transform.position;
			Destroy(gameObject);
		};

		StartCoroutine (ShootFireball ());
		StartCoroutine (ResetPath ());
	}
	
	// Update is called once per frame
	void Update () {
		lastDirection = unit2D.movement;
	}

	void FlashColor(float r, float g, float b, float seconds) {
		StartCoroutine (FlashColorCoroutine (r, g, b, seconds));
	}

	IEnumerator FlashColorCoroutine(float r, float g, float b, float seconds) {
		spriteRenderer.color = new Color(r, g, b);
		yield return new WaitForSeconds (seconds);
		spriteRenderer.color = new Color(1, 1, 1);
	}

	Vector3 GetBulletPoint() {
		Vector2 newPos = lastDirection.normalized * 0.5f;

		newPos.x += transform.position.x;
		newPos.y += transform.position.y;

		return new Vector3 (newPos.x, newPos.y, 0);
	}

	IEnumerator ShootFireball() {
		while(true) {
			yield return new WaitForSeconds (4.5f);

			GameObject fireball = Instantiate (fireballPrefab);

			fireball.transform.position = GetBulletPoint();
			fireball.GetComponent<Fireball>().direction = lastDirection.normalized;
			fireball.GetComponent<Fireball>().speed = 1.5f;
		}
	}

	IEnumerator ResetPath() {
		while (true) {
			if (targetObject != null) {
				Vector2 targetPos = targetObject.transform.position;
				unit2D.FollowPathToPoint (targetPos);
			} else {
				unit2D.StopFollowingPath ();
			}
			yield return new WaitForSeconds (1f);
		}
	}
}
