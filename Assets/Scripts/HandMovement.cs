using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {
	GameObject player;
	bool moving = false;
	float distance;
	bool horizontal;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (transform.position.y < 35f || transform.position.y > 42.3f)
			horizontal = true;
		else
			horizontal = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!moving){
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
					StartCoroutine (appear (transform.position, vertical_direction, horizontal_direction));
				else
					StartCoroutine (appear (transform.position, horizontal_direction, vertical_direction));
				
		}
		
		}
	}

	IEnumerator appear(Vector3 start, Vector3 firstDir, Vector3 secondDir){
		moving = true;
		Vector3 dest = start + firstDir * 2f;
		for (float t = 0; t < 1f; t += Time.deltaTime){
			transform.position = Vector3.Lerp (start, dest, t / 1f);
			yield return null;
		}
		transform.position = dest;
		start = dest;
		dest = transform.position + secondDir * 3f;
		for (float t = 0; t < 1.5f; t += Time.deltaTime){
			transform.position = Vector3.Lerp (start, dest, t / 1.5f);
			yield return null;
		}
		transform.position = dest;
		start = dest;
		dest = transform.position + -firstDir * 2f;
		for (float t = 0; t < 1f; t += Time.deltaTime){
			transform.position = Vector3.Lerp (start, dest, t / 1f);
			yield return null;
		}
		transform.position = dest;
		moving = false;
		if (transform.position.y < 35f || transform.position.y > 42.3f)
			horizontal = true;
		else
			horizontal = false;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.GetComponent<Player>())
		{
			StartCoroutine(TempIgnore(other));
		}
	}

	IEnumerator TempIgnore(Collision collision)
	{
		Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
		yield return new WaitForSeconds(collision.gameObject.GetComponent<Player>().flashTime);
		Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, false);
	}
}
