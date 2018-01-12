using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int currentHealth = 6;
	public int maxHealth = 6;
	public float flashTime = 1f;
	public float force = 400f;
	public float knockBackTime = 0.3f;
	private Animator anim;
	Rigidbody rb;
	public bool invincible = false;
	public bool movement = true;
	public bool dead = false;
	ChangeHealth changeHealthScript;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		changeHealthScript = GameObject.Find ("HeartManager").GetComponent<ChangeHealth> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

	}

	void OnCollisionEnter(Collision collision){
		OnHit (collision);
	}
	public void OnHit(Collision collision){
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
				StartCoroutine (KnockBack (collision));
			}

		}
	}

	IEnumerator Flash(){
//		anim.SetTrigger ("Flash");
		invincible = true;
		yield return new WaitForSeconds (flashTime);
		invincible = false;
	}

	IEnumerator KnockBack(Collision collision){
		movement = false;
		rb.AddForce(force * collision.contacts [0].normal);
		Debug.Log (force * collision.contacts [0].normal);
		yield return new WaitForSeconds (knockBackTime);
		movement = true;
	}

	void Die(){
		dead = true;
		anim.SetBool ("Dead", true);
	}
}
