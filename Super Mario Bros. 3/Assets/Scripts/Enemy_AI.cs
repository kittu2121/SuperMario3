﻿using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {
	public LayerMask GroundLayers;
	private Transform is_on_ground;
	public bool canJump;
	public bool canJump2;
	// Use this for initialization
	void Start () {
		GetComponent<PE_Obj2D>().vel.x = -2.0f;
		transform.localScale = new Vector3(-1, 1, 1);
		is_on_ground = transform.FindChild("IsOnGround");
	}
	// Update is called once per frame
	void Update () {
		Vector2 point1 = new Vector2(is_on_ground.transform.position.x - is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y - is_on_ground.collider2D.bounds.size.y/2);
		Vector2 point2 = new Vector2(is_on_ground.transform.position.x + is_on_ground.collider2D.bounds.size.x/2, 
		                             is_on_ground.transform.position.y + is_on_ground.collider2D.bounds.size.y/2);
		canJump = Physics2D.OverlapArea(point1, point2, GroundLayers, 0, 0);
		// next bool needed so that you can't jump off walls
		canJump2 = Physics2D.OverlapPoint(is_on_ground.position, GroundLayers);
		// go in opposite direction if the enemy approaches a cliff

		if (!canJump2){
			GetComponent<PE_Obj2D>().acc.y = -60.0f;
			// terminal velocity
			if (GetComponent<PE_Obj2D>().vel.y <= -20.0f) {
				GetComponent<PE_Obj2D>().acc.y = 0;
				GetComponent<PE_Obj2D>().vel.y = -20.0f;
			}
		}
		if (!canJump2 && GetComponent<PE_Obj2D>().acc.y == 0) {
			GetComponent<PE_Obj2D>().vel.x = -GetComponent<PE_Obj2D>().vel.x;
			transform.localScale = new Vector3(Mathf.Sign(GetComponent<PE_Obj2D>().vel.x), 1, 1);
		}
		// canJump2 = canJump2 || (Mathf.Abs (GetComponent<PE_Obj2D>().acc.y) < 0.1f);
	}
}
