using UnityEngine;
using System.Collections;

public class Enemy_Hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player") {
			transform.parent.GetComponent<PE_Obj2D>().vel.x = -transform.parent.GetComponent<PE_Obj2D>().vel.x;
			transform.localScale = new Vector3(Mathf.Sign(transform.parent.GetComponent<PE_Obj2D>().vel.x), 1, 1);
		}
	}
	void OnTriggerExit2D(Collider2D other) {
	}
	// Update is called once per frame
	void Update () {
	
	}
}
