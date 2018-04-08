using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	Animator animator;
	BoxCollider2D boxCollider;
	Durability durability;
	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;

	float speed;
	Vector2 deltaForce;
	Vector2 lastDirection;

	public GameObject fireballPrefab;

	float actionTime;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		durability = GetComponent<Durability> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		speed = 0.79f;
		actionTime = float.NegativeInfinity;

		durability.maxHealthPoints = 3;
		durability.healthPoints = 3;

		durability.onDamaged = (int diff) => {
			GameObject.Find("GamePlay").GetComponent<AudioPlayer>().PlayHurtSound();
			FlashColor(1, 0, 0, 0.2f);
		};
		durability.onHealed = (int diff) => {
			FlashColor(0, 1, 0, 0.2f);
		};
		durability.onDestruction = (int diff) => {
			//TODO: play dying sound
			Destroy(gameObject);
			GameObject.Find("GamePlay").GetComponent<GamePlay>().GameOver();
		};

		//hackfix
		lastDirection = new Vector2(0, -1);
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
		Move ();
		SendAnimationInfo ();
	}

	void CheckInput() {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		deltaForce = new Vector2 (horizontal, vertical);

		if (deltaForce != Vector2.zero) {
			lastDirection = deltaForce;
		}

		//shooting
		if (Input.GetButton ("Shoot") && Time.time - actionTime > 0.5f) {
			GameObject fireball = Instantiate (fireballPrefab);

			//position
			fireball.transform.position = GetBulletPoint();

			//direction
			fireball.GetComponent<Fireball>().direction = lastDirection.normalized;

			//speed
			fireball.GetComponent<Fireball>().speed = 1.5f;

			//audio
			GameObject.Find("GamePlay").GetComponent<AudioPlayer>().PlayShootSound();

			actionTime = Time.time;
		}
	}

	void Move() {
		rigidBody.velocity = Vector2.zero;
		rigidBody.AddForce (deltaForce.normalized * speed, ForceMode2D.Impulse);
	}

	void SendAnimationInfo() {
		animator.SetFloat ("xSpeed", lastDirection.x);
		animator.SetFloat ("ySpeed", lastDirection.y);
		animator.SetBool ("isMoving", deltaForce != Vector2.zero);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<Monster> () != null) {
			durability.healthPoints = 0;
		}
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
		Vector2 newPos = lastDirection.normalized * 0.3f;

		//hackfix
		if (Mathf.Abs(lastDirection.x) == Mathf.Abs(lastDirection.y)) {
			newPos = lastDirection.normalized * 0.4f;
		}

		newPos.x += transform.position.x;
		newPos.y += transform.position.y;

		return new Vector3 (newPos.x, newPos.y, 0);
	}
}
