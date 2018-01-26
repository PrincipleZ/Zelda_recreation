using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHurt : MonoBehaviour {

    public AudioClip hurtSound;
	public bool damageTaken;
	public int damageAmount;
	public bool movement = true;
	public float knockBackTime = 0.3f;
	public float force = 300f;
	public bool invincible = false;
	Rigidbody rb;

	bool isHoriz;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	public void OnHit(Collider other){
		if (!invincible) {
			GameObject other_go = other.gameObject;

			if (other_go.tag == "sword") {
				damageTaken = true;
				damageAmount = 1;
				StartCoroutine (KnockBack (other));
			} else if (other_go.tag == "arrow") {
				damageTaken = true;
				damageAmount = 2;
				StartCoroutine (KnockBack (other));

			} else if (other_go.tag == "magicSword") {
				damageTaken = true;
				damageAmount = 1;
				StartCoroutine (KnockBack (other));
			} else if (other_go.tag == "boomerang") {

				if (GetComponent<EnemyHealth> ().maxHealth == 1) {
					damageTaken = true;
					damageAmount = 1;
				} else {
					StartCoroutine (Stun ());
					AudioSource.PlayClipAtPoint (hurtSound, Camera.main.transform.position);
				}
			}
		}
	}
	IEnumerator Stun(){
		rb.velocity = Vector3.zero;
		movement = false;
		yield return new WaitForSeconds (2f);
		movement = true;
	}

	IEnumerator KnockBack(Collider collider){
		invincible = true;
		movement = false;
		Vector3 knockBackDir = (transform.position - collider.ClosestPointOnBounds(transform.position)).normalized;
		//Normalize to only grid knockback
		Debug.Log(knockBackDir);
		if(Mathf.Abs(knockBackDir.x) > Mathf.Abs(knockBackDir.y))
		{
			isHoriz = true;
		}
		else
		{
			isHoriz = false;
		}

//		if (GetComponent<RandMovement>() != null && GetComponent<RandMovement>().path.x != 0)
//		{
//			if (isHoriz)
//			{
//				knockBackDir.y = 0;
//				if (knockBackDir.x < 0)
//				{
//					knockBackDir.x = -1;
//				}
//				else
//				{
//					knockBackDir.x = 1;
//				}
//			}
//			else
//			{
//				knockBackDir = Vector2.zero;
//			}
//
//		}
//		else
//		{
//			if (!isHoriz)
//			{
//				knockBackDir.x = 0;
//				if (knockBackDir.y < 0)
//				{
//					knockBackDir.y = -1;
//				}
//				else
//				{
//					knockBackDir.y = 1;
//				}
//			}
//			else
//			{
//				knockBackDir = Vector2.zero;
//			}
//		}
//		if(knockBackDir != Vector3.zero)
//		{
//			rb.velocity = Vector3.zero;
//			rb.AddForce(force * knockBackDir);
//
//			yield return new WaitForSeconds(knockBackTime);
//		}
		if (isHoriz){
			knockBackDir.y = 0;
			if (knockBackDir.x < 0)
			{
				knockBackDir.x = -1;
			}
			else
			{
				knockBackDir.x = 1;
			}
		} else{
			knockBackDir.x = 0;
			if (knockBackDir.y < 0)
			{
				knockBackDir.y = -1;
			}
			else
			{
				knockBackDir.y = 1;
			}


		}
		if (knockBackDir != Vector3.zero){
			rb.velocity = Vector3.zero;
			rb.AddForce (force * knockBackDir);
			yield return new WaitForSeconds (knockBackTime);
			rb.velocity = Vector3.zero;
		}
		movement = true;
		invincible = false;
	}

}
