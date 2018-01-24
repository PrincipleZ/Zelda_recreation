using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour {

	bool shot;
	bool back;
    bool loopTime = true;
    public AudioClip boomerAudio;
	Vector3 direction;
	Rigidbody rb;
	Animator animator;
	Transform parentTransform;
	Transform trans;
	public float force = 2f;
	public float backSpeed = 3f;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		Debug.Log (rb);
		animator = GetComponent<Animator> ();
		shot = true;
		back = false;
		trans = GetComponent<Transform> ();
		direction = Vector3.zero;
		parentTransform = transform;
	}


	public void shoot(Vector3 dir, Transform pTransform){
		direction = dir;
		parentTransform = pTransform;

		rb.velocity = force * direction;
		shot = true;
	}

	void FixedUpdate(){

        if (loopTime)
            StartCoroutine(BoomerSound());

		if (shot) {
			if (!back)
				rb.velocity -= direction / 10;
//		Debug.Log (rb.velocity);
			if (rb.velocity == Vector3.zero) {
				back = true;
			}
			if (back) {
				rb.velocity = backSpeed * (parentTransform.position - trans.position).normalized;
				if (Vector3.Distance (trans.position, parentTransform.position) < 0.2f) {
				
					Destroy (this.gameObject);
				}
			}
		}
	}

    IEnumerator BoomerSound()
    {
        loopTime = false;
        AudioSource.PlayClipAtPoint(boomerAudio, Camera.main.transform.position);
        yield return new WaitForSeconds(.15f);
        loopTime = true;
    }

	void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject);
		if (other.gameObject.tag == "wall" || other.gameObject.tag == "door(wall)")
        {
			//play hit wall effect
			if (!back){
				back = true;
				rb.velocity = Vector3.zero;
			}
		}
        else if (other.gameObject.tag != "enemy")
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
        else {
			if (!back){
				back = true;
				rb.velocity = Vector3.zero;
			}
		}

	}
}
