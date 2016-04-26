using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	float playerPosition;
	private float startingY;
	private float offset;

	void Start () {
		Vector3 position = new Vector3 (0.0f, 0.0f, -10);
		transform.position = position;
	}

	void LateUpdate () {
			Vector3 position = new Vector3 (0.0f, player.transform.position.y, -10);
			transform.position = position;
	}
}
