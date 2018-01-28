using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePortalScript : MonoBehaviour {

	public Sprite north;
	public Sprite east;
	public Sprite south;
	public Sprite west;
	public float pushSpeed;
	public string warpObject = null;
	public GameObject sword;
	public GameObject arrow;

	GameObject player;
	GameObject BluePortal;
	public float swordRotationAmount;
	public float arrowRotationAmount;

	public Vector3 pushDir;

	void Start () {
		player = GameObject.Find ("Player");
		warpObject = null;
	}
		
	void Update () {
		BluePortal = GameObject.FindWithTag ("BluePortal");
		if (BluePortal == null) {
			transform.parent.GetComponent<Collider> ().enabled = true;
		} else {
			transform.parent.GetComponent<Collider> ().enabled = false;
		}
	}


	void OnTriggerEnter(Collider other){

		if (BluePortal != null && BluePortal.GetComponent<BluePortalScript> ().warpObject == null) {
			warpObject = other.tag;
		}

		if (other.gameObject.tag == "Player"){
			if (BluePortal.GetComponent<BluePortalScript> ().warpObject == "Player") {
				StartCoroutine (Pause (other));
				StartCoroutine(Push ());
			} 
			else {
				other.transform.position = BluePortal.transform.position;
                other.GetComponent<playerSounds>().PortalTravel();
                warpObject = "Player";
				if (GetComponent<cameraWarp>() != null)
					GetComponent<cameraWarp> ().warpCamera ();
				
			}
		}else if (other.gameObject.name == "Sword(Clone)") {
			if (BluePortal.GetComponent<BluePortalScript> ().warpObject == "sword") {
				StartCoroutine(Pause (other));
			}
			else{
                GameObject.Find("Player").GetComponent<playerSounds>().PortalTravel();
                Destroy(other.gameObject);
				GameObject temp = Instantiate (sword, BluePortal.transform.position , Quaternion.identity);
				temp.transform.Rotate (0, 0, BluePortal.GetComponent<BluePortalScript> ().swordRotationAmount);
				temp.transform.position += temp.transform.right * .5f;
				temp.GetComponent<Rigidbody> ().velocity = temp.transform.right * 10;
			}
		}else if (other.gameObject.name == "Arrow(Clone)") {
			if (BluePortal.GetComponent<BluePortalScript> ().warpObject == "arrow") {
				StartCoroutine(Pause (other));
			}
			else{
                GameObject.Find("Player").GetComponent<playerSounds>().PortalTravel();
                Destroy(other.gameObject);
				GameObject temp = Instantiate (arrow, BluePortal.transform.position , Quaternion.identity);
				temp.transform.Rotate (0, 0, BluePortal.GetComponent<BluePortalScript> ().arrowRotationAmount);
				temp.transform.position += temp.transform.up * .5f;
				temp.GetComponent<Rigidbody> ().velocity = temp.transform.up * 10;
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

	IEnumerator Push(){
		player.GetComponent<Player> ().movement = false;
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		player.GetComponent<Rigidbody> ().AddForce (200 * pushDir);
		yield return new WaitForSeconds (.2f);
		player.GetComponent<Player> ().movement = true;
	}

	IEnumerator Pause(Collider other){
		
		yield return new WaitForSeconds (.01f);
		BluePortal.GetComponent<BluePortalScript> ().warpObject = null;

	}

	IEnumerator PauseCamera(){
		cameraWarp cameraScript = BluePortal.GetComponent<cameraWarp>();
		if (cameraScript != null){
			cameraScript.enabled = false;
		}
		yield return new WaitForSeconds (2f);
		if (cameraScript != null){
			cameraScript.enabled = true;
		}
	}
}