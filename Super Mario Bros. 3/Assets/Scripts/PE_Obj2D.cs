using UnityEngine;
using System.Collections;



public class PE_Obj2D : MonoBehaviour {
	public bool			still = false;
	public PE_Collider2D	coll = PE_Collider2D.aabb;
	public PE_GravType2D	grav = PE_GravType2D.constant;
	
	public Vector2		acc = Vector2.zero;

	public Vector2		vel = Vector2.zero;
	public Vector2		vel0 = Vector2.zero;

	public Vector2		pos0 = Vector2.zero;
	public Vector2		pos1 = Vector2.zero;
	public Vector2		thatP = Vector2.zero;
	public Vector2		delta = Vector2.zero;
	

	void Start() {
		if (PhysEngine2D.objs.IndexOf(this) == -1) {
			PhysEngine2D.objs.Add(this);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other) {
		// Ignore collisions of still objects
		if (still) return;

		PE_Obj2D otherPEO = other.GetComponent<PE_Obj2D>();
		if (otherPEO == null) return;

		ResolveCollisionWith(otherPEO);
	}

	void OnTriggerStay2D(Collider2D other) {
		OnTriggerEnter2D(other);
	}

	void ResolveCollisionWith(PE_Obj2D that) {
		// Assumes that "that" is still

		switch (this.coll) {

		case PE_Collider2D.aabb:

			switch (that.coll) {
			case PE_Collider2D.aabb:

				// AABB / AABB collision
				float eX1, eY1, eX2, eY2, dX, dY, eX0, eY0;

				//Vector2 overlap = Vector2.zero;
				thatP = that.transform.position;
				delta = pos1 - thatP;
				if ((this.transform.position.x <= thatP.x + that.collider2D.bounds.size.x/2 ) &&
				    (this.transform.position.x >= thatP.x - that.collider2D.bounds.size.x/2 )) { // if the center of this obj is between the x-bounds of that obj

					if (pos1.y >= thatP.y) { // land on top
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						acc.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y + dist+0.01f);
						this.transform.position = pos;
					}
					else { // hit the bottom
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y - dist-0.03f);
						this.transform.position = pos;
					}
				}

				else if (delta.x >= 0 && delta.y >= 0) { // Top, Right
					// Get the edges that we're concerned with
					eX0 = pos0.x - this.collider2D.bounds.size.x / 2; // prev Left side of object.
					eY0 = pos0.y - this.collider2D.bounds.size.y / 2; // prev bottom side
					eX1 = pos1.x - this.collider2D.bounds.size.x / 2; // current right side
					eY1 = pos1.y - this.collider2D.bounds.size.y / 2; // current bottom side
					eX2 = thatP.x + that.collider2D.bounds.size.x / 2 ; // other object's right side 
					eY2 = thatP.y + that.collider2D.bounds.size.y / 2 ; // other object's  top side.
					if ((Mathf.Abs(eY0 - eY2) <= 0.1)) {// land on top
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						acc.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y + dist + 0.01f);
						this.transform.position = pos;
					}
					else { // hit the right side
						float dist = this.collider2D.bounds.size.x/2 + that.collider2D.bounds.size.x/2;
						vel.x = 0;
						acc.x = 0;
						Vector2 pos = new Vector2(that.transform.position.x + dist, this.transform.position.y);
						this.transform.position = pos;
					}

				} else if (delta.x >= 0 && delta.y < 0) { // Bottom, Right
					eX0 = pos0.x - this.collider2D.bounds.size.x / 2;
					eY0 = pos0.y + this.collider2D.bounds.size.y / 2;
					eX1 = pos1.x - this.collider2D.bounds.size.x / 2;
					eY1 = pos1.y + this.collider2D.bounds.size.y / 2;
					eX2 = thatP.x + that.collider2D.bounds.size.x / 2 ;
					eY2 = thatP.y - that.collider2D.bounds.size.y / 2 ;

					//print (eY1); print (eY2);
					//if ((mThis > mThat) || ((eX0 == eX1) && (Mathf.Abs (mThat) <= 0.1) && (eX0 != eX2))) {// hit the bottom
					if ((Mathf.Abs(eY1 - eY2) <= 0.1) && (Mathf.Abs(eX1 - eX2) >= 0.4)) {// hit the bottom
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y - dist - 0.03f);
						this.transform.position = pos;
					}
					else { // hit the right side
						float dist = this.collider2D.bounds.size.x/2 + that.collider2D.bounds.size.x/2;
						vel.x = 0;
						acc.x = 0;
						Vector2 pos = new Vector2(that.transform.position.x + dist + 0.06f, this.transform.position.y);
						this.transform.position = pos;
					}
				} else if (delta.x < 0 && delta.y < 0) { // Bottom, Left
					eX0 = pos0.x + this.collider2D.bounds.size.x / 2;
					eY0 = pos0.y + this.collider2D.bounds.size.y / 2;
					eX1 = pos1.x + this.collider2D.bounds.size.x / 2;
					eY1 = pos1.y + this.collider2D.bounds.size.y / 2;
					eX2 = thatP.x - that.collider2D.bounds.size.x / 2 ;
					eY2 = thatP.y - that.collider2D.bounds.size.y / 2 ;

					if ((Mathf.Abs(eY1 - eY2) <= 0.1) && (Mathf.Abs(eX1 - eX2) >= 0.4)) {// hit the bottom
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y - dist - 0.03f);
						this.transform.position = pos;
					}
					else { // hit the left side
						float dist = this.collider2D.bounds.size.x/2 + that.collider2D.bounds.size.x/2;
						vel.x = 0;
						acc.x = 0;
						Vector2 pos = new Vector2(that.transform.position.x - dist - 0.06f, this.transform.position.y);
						this.transform.position = pos;
					}
				} else if (delta.x < 0 && delta.y >= 0) { // Top, Left
					eX0 = pos0.x + this.collider2D.bounds.size.x / 2;
					eY0 = pos0.y - this.collider2D.bounds.size.y / 2;
					eX1 = pos1.x + this.collider2D.bounds.size.x / 2;
					eY1 = pos1.y - this.collider2D.bounds.size.y / 2;
					eX2 = thatP.x - that.collider2D.bounds.size.x / 2 ;
					eY2 = thatP.y + that.collider2D.bounds.size.y / 2 ;

					if (Mathf.Abs(eY1 - eY2) <= 0.1) {// land on top
						float dist = this.collider2D.bounds.size.y/2 + that.collider2D.bounds.size.y/2;
						vel.y = 0;
						acc.y = 0;
						Vector2 pos = new Vector2(this.transform.position.x, that.transform.position.y + dist + 0.01f);
						this.transform.position = pos;
					}
					else { // hit the left side
						float dist = this.collider2D.bounds.size.x/2 + that.collider2D.bounds.size.x/2;
						vel.x = 0;
						acc.x = 0;
						Vector2 pos = new Vector2(that.transform.position.x - dist, this.transform.position.y);
						this.transform.position = pos;
					}
				}


				break;
			}

			break;
		}
	}


}
