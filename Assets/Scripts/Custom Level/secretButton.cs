using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretButton : MonoBehaviour {
	public GameObject door;
	public Sprite unlocked;
	public Sprite locked;


	void OnTriggerEnter(Collider other){
		door.GetComponent<BoxCollider> ().enabled = false;
		door.GetComponent<SpriteRenderer> ().sprite = unlocked;
	}

	void OnTriggerExit(Collider other){
		
		door.GetComponent<BoxCollider> ().enabled = true;
		door.GetComponent<SpriteRenderer> ().sprite = locked;
	}
}
