using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	public bool canJump;
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		canJump = true;
	}
	void OnTriggerStay2D(Collider2D other) {
		canJump = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		canJump = false;
	}
}
