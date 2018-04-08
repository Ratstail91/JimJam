using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	AudioSource audioSource;

	public AudioClip coinSound;
	public AudioClip shootSound;
	public AudioClip hurtSound;
	public AudioClip killSound;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Play(AudioClip clip) {
		audioSource.PlayOneShot (clip, 1.0f);
	}

	//Oh wow this is terrible
	public void PlayCoinSound() {
		Play (coinSound);
	}

	public void PlayShootSound() {
		Play (shootSound);
	}

	public void PlayHurtSound() {
		Play (hurtSound);
	}

	public void PlayKillSound() {
		Play (killSound);
	}
}
