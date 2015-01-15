using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float HorizSpeed = Input.GetAxis ("Horizontal");
		if (HorizSpeed > 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (HorizSpeed < 0) {
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
