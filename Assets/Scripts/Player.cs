using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int currentHealth = 6;
	public int maxHealth = 6;
	public float flashTime = 1f;
	public float force = 50f;
	public float knockBackTime = 0.3f;
	private Animator anim;
	Rigidbody rb;
	public bool invincible = false;
	public bool movement = true;
	public bool dead = false;
	ChangeHealth changeHealthScript;
	RoomSwitch cameraScript;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentHealth = maxHealth;
		changeHealthScript = GameObject.Find ("HeartManager").GetComponent<ChangeHealth> ();
		anim = GetComponent<Animator> ();
		cameraScript = GameObject.FindWithTag ("MainCamera").GetComponent<RoomSwitch> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

	}

	void OnCollisionEnter(Collision collision){
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag("Enemy"))
			OnHit (collider);
		if (collider.gameObject.CompareTag("entrance") && !cameraScript.switching) {
			Vector3 dir = Vector3.zero;
			switch (collider.gameObject.name){
			// switch to right
			case "Tile_R_EN":
				dir = new Vector3 (1, 0, 0);
				break;
			case "Tile_L_EN":
				dir = new Vector3 (-1, 0, 0);			
				break;
			case "Tile_UPEN":
				dir = new Vector3 (0, 1, 0);			
				break;
			case "Tile_DNEN":
				dir = new Vector3 (0, -1, 0);			
				break;

			}
			cameraScript.switchControl (dir);
			StartCoroutine (waitForCamera(dir));

		}
	}
		
	public void OnHit(Collider collider){
		if (!invincible && !dead) {
			currentHealth -= 1;
			changeHealthScript.change (currentHealth);
			if (currentHealth == 0) {
				movement = false;
				Die ();
			}
			else{
				Debug.Log (currentHealth);
				StartCoroutine (Flash ());
				StartCoroutine (KnockBack (collider));
			}

		}
	}
	IEnumerator waitForCamera(Vector3 direction){
		Vector3 start = transform.position;
		movement = false;
		for (float t = 0f; t < cameraScript.cameraSwitchTime; t += Time.deltaTime){
			transform.position = Vector3.Lerp (start, start + direction*2, t/cameraScript.cameraSwitchTime);
			yield return null;
		}
		Debug.Log (transform.position);
	

		movement = true;
	}

	IEnumerator Flash(){
//		anim.SetTrigger ("Flash");
		invincible = true;
		yield return new WaitForSeconds (flashTime);
		invincible = false;
	}

	IEnumerator KnockBack(Collider collider){
		movement = false;
		Vector3 knockBackDir = (transform.position - collider.ClosestPointOnBounds(transform.position)).normalized;
		if (knockBackDir == Vector3.zero)
			knockBackDir = new Vector3 (0, -1, 0);
		rb.AddForce(force * knockBackDir);
		Debug.Log (force * knockBackDir);
		yield return new WaitForSeconds (knockBackTime);
		movement = true;
	}

	void Die(){
		dead = true;
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		anim.SetBool ("Dead", true);
		anim.speed = 1;
	}
}
