using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour {
	public LayerMask GroundLayers;
	private Transform is_on_ground;
	public bool canJump;
	public bool canJump2;
	public bool mushroom;
	private bool destroyed = false;
	// Use this for initialization
	void Start () {
		GetComponent<PE_Obj2D>().vel.x = -3.0f;
		transform.localScale = new Vector3(-1, 1, 1);
		is_on_ground = transform.FindChild("IsOnGround");
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (destroyed)
			Destroy (transform.gameObject);
		Vector2 point1 = new Vector2(is_on_ground.transform.position.x - is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y - is_on_ground.collider2D.bounds.size.y/2);
		Vector2 point2 = new Vector2(is_on_ground.transform.position.x + is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y + is_on_ground.collider2D.bounds.size.y/2);
		canJump = Physics2D.OverlapArea(point1, point2, GroundLayers, 0, 0);
		// next bool needed so that you can't jump off walls
		canJump2 = Physics2D.OverlapPoint(is_on_ground.position, GroundLayers);
		if (!canJump2){
			GetComponent<PE_Obj2D>().acc.y = -60.0f;
			// terminal velocity
			if (GetComponent<PE_Obj2D>().vel.y <= -20.0f) {
				GetComponent<PE_Obj2D>().acc.y = 0;
				GetComponent<PE_Obj2D>().vel.y = -20.0f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player") {
			transform.GetComponent<PE_Obj2D>().vel.x = -transform.GetComponent<PE_Obj2D>().vel.x;
			transform.localScale = new Vector3(Mathf.Sign(transform.GetComponent<PE_Obj2D>().vel.x), 1, 1);
		}
		else{
			PhysEngine2D.objs.Remove(transform.gameObject.GetComponent<PE_Obj2D>());
			Destroy (transform.gameObject);
			destroyed = true;
		}
	}
}
