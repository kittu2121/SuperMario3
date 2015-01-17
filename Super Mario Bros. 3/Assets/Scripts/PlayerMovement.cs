using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float SpeedMultiplier = 3.0f;
	public LayerMask GroundLayers;
	private Animator mario_anim;
	private Transform is_on_ground;
	// Use this for initialization
	void Start () {
		mario_anim = GetComponent<Animator>();
		is_on_ground = transform.FindChild("IsOnGround");
		//canJump = is_on_ground.GetComponent<Jump>().canJump;
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector2 point1 = new Vector2(is_on_ground.transform.position.x - is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y - is_on_ground.collider2D.bounds.size.y/2);
		Vector2 point2 = new Vector2(is_on_ground.transform.position.x + is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y + is_on_ground.collider2D.bounds.size.y/2);
		bool canJump = Physics2D.OverlapArea(point1, point2, GroundLayers, 0, 0);
		// next bool needed so that you can't jump off walls
		bool canJump2 = Physics2D.OverlapPoint(is_on_ground.position, GroundLayers);
		if (Input.GetButton ("Jump")) {
			if ((canJump) && (canJump2)){
				GetComponent<PE_Obj2D>().vel.y = 10.0f;
				canJump = false;
				canJump2 = false;
			}
		}
		if (!canJump){
			GetComponent<PE_Obj2D>().acc.y = -30.0f;
		}
		mario_anim.SetBool ("CanJump", canJump);
		float HorizSpeed = Input.GetAxis ("Horizontal");
		mario_anim.SetFloat("Speed", Mathf.Abs(HorizSpeed));
		if (HorizSpeed > 0) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (HorizSpeed < 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
		this.rigidbody2D.velocity = new Vector2(HorizSpeed*SpeedMultiplier, this.rigidbody2D.velocity.y);

	}
}
