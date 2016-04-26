using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {


	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0f;
	private Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
	}
	

	void LateUpdate () {
		ColorChanging ();
	}

	void ColorChanging() {
		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		rend.material.color = Color.Lerp (colorStart, colorEnd, lerp);
	}
} 
