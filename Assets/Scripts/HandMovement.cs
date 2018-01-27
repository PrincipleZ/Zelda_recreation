using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {
	GameObject player;
	public bool moving = false;
	float distance;
	bool horizontal;
	bool grabbed = false;
	Vector3 initPos;
	Coroutine move;
	Coroutine submove;
	GetHurt hurtScript;
	bool finished = false;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (transform.position.y < 35f || transform.position.y > 42.3f)
			horizontal = true;
		else
			horizontal = false;
		initPos = transform.position;
		hurtScript = GetComponent<GetHurt> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbed){
			player.GetComponent<Player> ().movement = false;
			player.transform.position = transform.position;
		}
		if (!hurtScript.movement && moving){
			if (move != null)
				StopCoroutine (move);
			if (submove != null)
				StopCoroutine (submove);
		}
		if (hurtScript.movement && moving){
			if (rb.velocity == Vector3.zero) {
				move = StartCoroutine (backHome ());
			}
		}

		if (!moving && hurtScript.movement){
			if (Vector3.Distance(transform.position, player.transform.position) < 3f){
				Vector3 direction = transform.position - player.transform.position;
				Vector3 horizontal_direction = Vector3.zero;
				Vector3 vertical_direction = Vector3.zero;
				if (direction.x > 0){
					horizontal_direction = new Vector3 (-1, 0, 0);
				} else{
					horizontal_direction = new Vector3 (1, 0, 0);
				}
				if (direction.y > 0){
					vertical_direction = new Vector3 (0, -1, 0);
				}else{
					vertical_direction = new Vector3 (0, 1, 0);
				}
				if (horizontal)
					move = StartCoroutine(appear (transform.position, vertical_direction, horizontal_direction));
				else
					move = StartCoroutine(appear (transform.position, horizontal_direction, vertical_direction));
				
		}
		
		}
	}

	IEnumerator appear(Vector3 start, Vector3 firstDir, Vector3 secondDir){
		bool interrupt = false;
		moving = true;
		Vector3 dest = start + firstDir * 2f;
		submove = StartCoroutine(movingPhase (start, dest, 1f));

		while(!finished){
			yield return null;
		}

		start = dest;
		dest = transform.position + secondDir * 3f;
		submove = StartCoroutine(movingPhase (start, dest, 1.5f));
		while(!finished){
			yield return null;
		}

		start = dest;
		dest = transform.position + -firstDir * 2f;
		submove = StartCoroutine(movingPhase (start, dest, 1f));
		while(!finished){
			yield return null;
		}

		moving = false;
		if (grabbed) {
			warpPlayer ();
			grabbed = false;
			Physics.IgnoreCollision(this.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
		}

		if (transform.position.y < 35f || transform.position.y > 42.3f)
			horizontal = true;
		else
			horizontal = false;
	}

	IEnumerator movingPhase(Vector3 start, Vector3 dest, float duration){
		finished = false;
		Vector3 dir = dest - start;
		while (Mathf.Abs(Vector3.Distance(dest, transform.position)) > 0.1f){
			rb.velocity = dir * 1f;
			yield return new WaitForFixedUpdate ();
		}

		transform.position = dest;
		finished = true;
        GetComponent<GetHurt>().enabled = true;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.GetComponent<BlockEnemies>()){
			if (moving) {
				StopAllCoroutines ();
				moving = false;
			}
			transform.position = initPos;
				

		}
		if (other.gameObject.GetComponent<Player>())
		{
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider, true);
			if (!other.gameObject.GetComponent<Player>().invincible && other.gameObject.GetComponent<Player>().movement)
            {
                other.gameObject.GetComponent<playerSounds>().GotHit();
                other.gameObject.GetComponent<Player>().currentHealth -= GetComponent<EnemyDamage>().damageAmount;
                other.gameObject.GetComponent<Player>().changeHealthScript.change(other.gameObject.GetComponent<Player>().currentHealth);
                grabbed = true;
				GetComponent<GetHurt>().enabled = false;
            }      
			else{
				Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider, false);
			}
		}
	}

	IEnumerator backHome(){


		while (transform.position != initPos) {
			if (horizontal) {
				if (transform.position.y - initPos.y > 0) {
					rb.velocity = new Vector3 (0, -2, 0);
				} else {
					rb.velocity = new Vector3 (0, 2, 0);
				}
			} else {
				if (transform.position.x - initPos.x > 0)
					rb.velocity = new Vector3 (-2, 0, 0);
				else
					rb.velocity = new Vector3 (2, 0, 0);
			}
			yield return null;
		}
		moving = false;
	}

	void warpPlayer(){
		player.transform.position = new Vector3 (39f, 6.5f, 0);
		GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (39.5f, 7.5f, -10);
		player.GetComponent<Player> ().movement = true;
	}

}
