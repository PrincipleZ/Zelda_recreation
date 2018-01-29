using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalScript : MonoBehaviour {

	public Sprite north;
	public Sprite east;
	public Sprite south;
	public Sprite west;
	public float pushSpeed;
	public string warpObject = null;
	public GameObject sword;
	public GameObject arrow;
	public LayerMask terrain;

	GameObject player;
	GameObject OrangePortal;
	public float swordRotationAmount;
	public float arrowRotationAmount;

	public Vector3 pushDir;

	void Start () {
		player = GameObject.Find ("Player");
		warpObject = null;
		if (Physics.Raycast (transform.position, pushDir, 0.6f, terrain)) {
			this.transform.parent.GetComponent<Collider> ().enabled = true;
			Destroy (this.gameObject);
		}
	}

	void Update () {
		OrangePortal = GameObject.FindWithTag ("OrangePortal");
		if (OrangePortal == null) {
			transform.parent.GetComponent<Collider> ().enabled = true;
		} else {
			transform.parent.GetComponent<Collider> ().enabled = false;
		}
	}


	void OnTriggerEnter(Collider other){
		Debug.Log (other);
		if (OrangePortal != null && OrangePortal.GetComponent<OrangePortalScript> ().warpObject == null) {
			warpObject = other.gameObject.tag;
		}
		if (OrangePortal != null) {
			if (other.gameObject.tag == "Player") {
				if (OrangePortal.GetComponent<OrangePortalScript> ().warpObject == "Player") {
					StartCoroutine (Pause (other));
					StartCoroutine (Push ());
				} else {
					other.transform.position = OrangePortal.transform.position + (OrangePortal.GetComponent<OrangePortalScript> ().pushDir / 10f);
					other.GetComponent<playerSounds> ().PortalTravel ();
					warpObject = "Player";
					if (GetComponent<cameraWarp> () != null)
						GetComponent<cameraWarp> ().warpCamera ();
				}
			} else if (other.gameObject.name == "Sword(Clone)") {
				if (OrangePortal.GetComponent<OrangePortalScript> ().warpObject == "sword") {
					StartCoroutine (Pause (other));
				} else {
					GameObject.Find ("Player").GetComponent<playerSounds> ().PortalTravel ();
					Destroy (other.gameObject);
					GameObject temp = Instantiate (sword, OrangePortal.transform.position, Quaternion.identity);
					temp.transform.Rotate (0, 0, OrangePortal.GetComponent<OrangePortalScript> ().swordRotationAmount);
					temp.transform.position += temp.transform.right * .5f;
					temp.GetComponent<Rigidbody> ().velocity = temp.transform.right * 10;
				}
			} else if (other.gameObject.name == "Arrow(Clone)") {
				if (OrangePortal.GetComponent<OrangePortalScript> ().warpObject == "arrow") {
					StartCoroutine (Pause (other));
				} else {
					GameObject.Find ("Player").GetComponent<playerSounds> ().PortalTravel ();
					Destroy (other.gameObject);
					GameObject temp = Instantiate (arrow, OrangePortal.transform.position, Quaternion.identity);
					temp.transform.Rotate (0, 0, OrangePortal.GetComponent<OrangePortalScript> ().arrowRotationAmount);
					temp.transform.position += temp.transform.up * .5f;
					temp.GetComponent<Rigidbody> ().velocity = temp.transform.up * 10;
				}
			} else if (other.gameObject.tag == "enemy" || other.gameObject.GetComponent<CustomPushableBlock> () != null) {
				other.gameObject.transform.position = OrangePortal.transform.position + OrangePortal.GetComponent<OrangePortalScript> ().pushDir;
			}
		}
	}



	public void PortalDirectionNESW(int direction){
		switch (direction) {
		case 0:
			pushDir = Vector3.up;
			transform.Rotate (0, 0, 180);
			arrowRotationAmount = 0;
			swordRotationAmount = 90f;
			break;
		case 1:
			pushDir = Vector3.right;
			transform.Rotate (0, 0, 90);
			arrowRotationAmount = -90f;
			swordRotationAmount = 0f;
			break;
		case 2:
			pushDir = Vector3.down;
			arrowRotationAmount = 180f;
			swordRotationAmount = -90f;
			break;
		case 3:
			pushDir = Vector3.left;
			transform.Rotate (0, 0, -90);
			arrowRotationAmount = 90f;
			swordRotationAmount = 180f;
			break;
		}
	}

	public IEnumerator Push(){
		player.GetComponent<Player> ().movement = false;
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		player.GetComponent<Rigidbody> ().AddForce (200 * pushDir);
		yield return new WaitForSeconds (.2f);
		player.GetComponent<Player> ().movement = true;
	}

	IEnumerator Pause(Collider other){
		cameraWarp cameraScript = GetComponent<cameraWarp>();
		if (cameraScript != null){
			cameraScript.enabled = false;
		}
		yield return new WaitForSeconds (.01f);
		OrangePortal.GetComponent<OrangePortalScript> ().warpObject = null;
		yield return new WaitForSeconds (1f);
		if (cameraScript != null){
			cameraScript.enabled = true;
		}
	}
	IEnumerator PauseCamera(){
		cameraWarp cameraScript = OrangePortal.GetComponent<cameraWarp>();
		if (cameraScript != null){
			cameraScript.enabled = false;
		}
		yield return new WaitForSeconds (2f);
		if (cameraScript != null){
			cameraScript.enabled = true;
		}
	}
}