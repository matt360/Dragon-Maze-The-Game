using UnityEngine;
using System.Collections;

public class SoundtrackController : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip gameSoundtrack;

	private PlayerController2D playerController2D;

	void Start () {
		GameObject playerController2DObject = GameObject.FindWithTag ("Player");

		if (playerController2DObject != null)
			playerController2D = playerController2DObject.GetComponent<PlayerController2D> ();
		if (playerController2DObject == null)
			Debug.Log ("Cannot find 'PlayerController2D' script");

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = gameSoundtrack;
		audioSource.Play ();
	}

	void Update () {
		if (playerController2D.game == false) {
			audioSource.Stop();
		}
	}
}
