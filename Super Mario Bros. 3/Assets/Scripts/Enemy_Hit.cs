using UnityEngine;
using System.Collections;

public class Enemy_Hit : MonoBehaviour {
	public GameObject camera;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player") {
			transform.GetComponent<PE_Obj2D>().vel.x = -transform.GetComponent<PE_Obj2D>().vel.x;
			transform.localScale = new Vector3(Mathf.Sign(transform.GetComponent<PE_Obj2D>().vel.x), 1, 1);
		}
		else {

			// damage Mario
			camera = GameObject.Find ("Main Camera");
			camera.GetComponent<Health>().gothurt = true;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
