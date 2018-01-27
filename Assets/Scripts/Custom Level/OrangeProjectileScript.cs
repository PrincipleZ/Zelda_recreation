using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeProjectileScript : MonoBehaviour {

	public Vector3 direction;
	public float moveSpeed = 3;
	public GameObject portal;
	public GameObject player;

	bool done = false;
	float horizontal_input;
	float vertical_input;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		player.GetComponent<ArrowKeyMovement> ().enabled = false;
		player.GetComponent<Animator> ().enabled = false;
		player.GetComponent<PortalGun> ().enabled = false;
		player.GetComponent<Bow> ().enabled = false;
		player.GetComponent<SwordDirection> ().isSwingingSword = true;
	}

	// Update is called once per frame
	void Update () {

		horizontal_input = Input.GetAxisRaw("Horizontal");
		vertical_input = Input.GetAxisRaw("Vertical");

		if (horizontal_input != 0 && direction.y != 0) {
			direction = new Vector3(horizontal_input, 0f, 0f);
		}
		else if(vertical_input != 0 && direction.x != 0){
			direction = new Vector3(0f, vertical_input, 0f);
		}

		transform.Translate (direction * moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (!done) {
			done = true;
			if (other.tag == "wallU") {
				OrientationNESW (2, other);
			} else if (other.tag == "wallR") {
				OrientationNESW (3, other);
			} else if (other.tag == "wallD") {
				OrientationNESW (0, other);
			} else if (other.tag == "wallL") {
				OrientationNESW (1, other);
			}
			player.GetComponent<ArrowKeyMovement> ().enabled = true;
			player.GetComponent<Animator> ().enabled = true;
			player.GetComponent<PortalGun> ().enabled = true;
			player.GetComponent<Bow> ().enabled = true;
			player.GetComponent<SwordDirection> ().isSwingingSword = false;
			Destroy (this.gameObject);
		} 

	}
	void OrientationNESW(int dir, Collider other){
		other.GetComponent<BoxCollider> ().enabled = false;
		GameObject temp = GameObject.Find ("Orange Portal(Clone)");
		if(temp != null)
			temp.transform.parent.GetComponent<BoxCollider> ().enabled = true;
		Destroy (temp);
		other.GetComponent<PWallScript> ().SpawnPortal ("orange", dir);
	}
}
