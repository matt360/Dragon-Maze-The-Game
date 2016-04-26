using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour 
{
	public float speed;
	private bool facingRight;
	private bool facingUp;
	public bool game;
	public AudioClip coinSound;
	public AudioClip deadSound;
	public AudioClip winSound;
	private AudioSource audioSource;

	private int score;
	public int scoreValue;

	private Rigidbody2D rb2d;

	Animator anim;

	public GUIText scoreText;
//	private ChasebarMover chasebarMover;

	private bool winGame;
	private bool gameOver;
	private bool restart;
	
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText winText;

	void Start () 
	{
//		GameObject chasebarMoverObject = GameObject.FindWithTag ("Chase");
//
//		if (chasebarMoverObject != null)
//			chasebarMover = chasebarMoverObject.GetComponent<ChasebarMover> ();
//		if (chasebarMoverObject == null)
//			Debug.Log ("Cannot find 'ChasebarMover' script");

		score = 0;
		facingRight = true;
		facingUp = true;
		game = true;
		UpdateScore ();

		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
		rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

		restartText.text = "";
		gameOverText.text = "";
		winText.text = "";
		
		winGame = false;
		gameOver = false;
		restart = false;
	}	

	void Update () 
	{
		UpdateScore ();

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		if (gameOver) {
			Restart ();
			restart = true;
//			rb2d.velocity = new Vector2(0.0f, 0.0f);
		}
		
		if (winGame) {
			Restart ();
			restart = true;
			rb2d.velocity = new Vector2(0.0f, 0.0f);
		}

		float moveVertical = Input.GetAxisRaw ("Vertical");
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");

			if (rb2d.velocity == Vector2.zero) {
			anim.SetBool ("Vertical", false);
			anim.SetBool ("Horizontal", false);

			if (!facingUp)
				FlipVertical ();
			if (!facingRight)
				FlipHorizontal ();

			if (game) {
				if (moveVertical > 0) {		
					anim.SetBool ("Vertical", true);

					if (!facingUp)
						FlipVertical ();
			
					rb2d.velocity = new Vector2 (0.0f, speed);
				} else if (moveVertical < 0) {
					anim.SetBool ("Vertical", true);

					if (facingUp)
						FlipVertical ();

					rb2d.velocity = new Vector2 (0.0f, -speed);;
				} else if (moveHorizontal < 0) {
					anim.SetBool ("Horizontal", true);
					if (facingRight)
						FlipHorizontal ();

					rb2d.velocity = new Vector2 (-speed, 0.0f);
				} else if (moveHorizontal > 0) {
					anim.SetBool ("Horizontal", true);

					if (!facingRight)
						FlipHorizontal ();

					rb2d.velocity = new Vector2 (speed, 0.0f);
				}
			}
		}
	}

	void FlipVertical ()
	{
		facingUp = !facingUp;
		Vector2 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
	
	void FlipHorizontal () 
	{
		facingRight = !facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			AddScore (scoreValue);
			audioSource.PlayOneShot (coinSound);
		} else if (other.gameObject.CompareTag ("Spikes")) {
			game = false;
			GameOver ();
			anim.SetBool ("Dead", true);
			audioSource.PlayOneShot (deadSound);
		} else if (other.gameObject.CompareTag ("Chase")) {
			game = false;
			GameOver ();
			anim.SetBool ("Dead", true);
			audioSource.PlayOneShot (deadSound);
		} else if (other.gameObject.CompareTag("Finish"))  {
			game = false;
			WinGame();
			audioSource.PlayOneShot(winSound);
			rb2d.velocity = new Vector2(0.0f, 0.0f);
		}
	}
	
	void AddScore (int newScoreValue) 
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore () 
	{
		scoreText.text = "" + score;
	}

	public void GameOver () 
	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
	}
	
	public void WinGame() {
		winText.text = "YOU DISCOVERED\nA BRAVE NEW, NON PIXELATED WORLD\nFROM THE FIRST SEMESTER!";
		gameOver = true;
	}
	
	void Restart () {
		restartText.text = "Press 'R' to restart\nPress 'ESC' to exit";
	}
}