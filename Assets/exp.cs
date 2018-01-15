using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp : MonoBehaviour {
	Rigidbody rb;
	int horizontal;
	int vertical;
	float speed;
	bool canMove;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		speed = 2f;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (Random.value < .25f) {
				horizontal = 1;
				vertical = 0;
			}
			else if (Random.value < .5f){
				horizontal = -1;
				vertical = 0;
			}else if (Random.value < .75f){
				horizontal = 0;
				vertical = 1;
			}else{
				horizontal = 0;
				vertical = -1;
			}


			StartCoroutine (move (new Vector2 (horizontal, vertical)));
		}
	}

	IEnumerator move(Vector2 dir){
		canMove = false;
		for (float t = 0; t < 1f; t += Time.deltaTime) {
			rb.velocity = speed * dir;
		}
		yield return new WaitForSeconds (1f);
		canMove = true;


	}
}
