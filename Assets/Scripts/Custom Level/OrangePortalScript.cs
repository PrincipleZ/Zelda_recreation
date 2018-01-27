using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePortalScript : MonoBehaviour {

	public Sprite north;
	public Sprite east;
	public Sprite south;
	public Sprite west;
	public float pushSpeed;

	GameObject player;
	GameObject BluePortal;
	float swordRotationAmount;
	float arrowRotationAmount;

	Vector3 pushDir;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		BluePortal = GameObject.Find ("Blue Portal(Clone)");
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"  && BluePortal.GetComponent<BluePortalScript> ().warpObject == "Player") {
			BluePortal.GetComponent<BluePortalScript> ().warpObject = null;
			player = other.gameObject;
			Debug.Log ("oknow");
		}
		//other.transform.position = BluePortal.transform.position;
	}

	public void PortalDirectionNESW(int direction){
		switch (direction) {
		case 0:
			pushDir = Vector3.up;
			arrowRotationAmount = 0;
			swordRotationAmount = -90f;
			break;
		case 1:
			pushDir = Vector3.right;
			arrowRotationAmount = 90f;
			swordRotationAmount = 0;
			break;
		case 2:
			pushDir = Vector3.down;
			arrowRotationAmount = 180;
			swordRotationAmount = 90f;
			break;
		case 3:
			pushDir = Vector3.left;
			arrowRotationAmount = -90;
			swordRotationAmount = 180f;
			break;
		}
	}

	void Push(){
		player.GetComponent<Player> ().movement = false;
		for (float i = 0; i < 1f / pushSpeed; i += Time.deltaTime) {
			player.GetComponent<Rigidbody> ().AddForce (100f, 0, 0);
			//player.transform.Translate (pushDir * pushSpeed * Time.deltaTime);
		}
	}
}