using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator mario_anim;
	// Use this for initialization
	void Start () {
		mario_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float HorizSpeed = Input.GetAxis ("Horizontal");
		mario_anim.SetFloat("Speed", Mathf.Abs(HorizSpeed));
		if (HorizSpeed > 0) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (HorizSpeed < 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}
}
