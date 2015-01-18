using UnityEngine;
using System.Collections;

public class Enemy_Death : MonoBehaviour {
	private Animator enemy_anim;
	public float timer;
	public bool dead = false;
	// Use this for initialization
	void Start () {
		enemy_anim = transform.parent.GetComponent<Animator>();
		timer = 10.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (timer >= 0.2f && timer <= 5.0f) {
			// PhysEngine2D.objs.Remove(transform.parent.gameObject.GetComponent<PE_Obj2D>());
			Destroy (transform.parent.gameObject);
		}
		timer += Time.fixedDeltaTime;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			dead = true;
			enemy_anim.SetBool ("Dead", dead);
			timer = 0;
			PhysEngine2D.objs.Remove(transform.parent.gameObject.GetComponent<PE_Obj2D>());
			transform.parent.gameObject.GetComponent<PE_Obj2D>().still = true;
		}
	}
}
