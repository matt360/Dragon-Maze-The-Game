using UnityEngine;
using System.Collections;

public class ChasebarMover : MonoBehaviour {

	private Rigidbody2D rb2d;
	private bool restart;

	public float speed;
	public GUIText restartText;


	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		restart = false;
		restartText.text = "";
	}

	void Update () {
		Vector2 move = new Vector2 (0.0f, speed);
		rb2d.velocity = move;
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.SetActive (false);
			speed = 0.0f;
			Restart ();
		}
	}

	void Restart () {
		restartText.text = "Press 'R' to restart\nPress 'ESC' to exit";
		restart = true;
	}
}