using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalScript : MonoBehaviour {

	GameObject OrangePortal;
	public Vector3 directionToEnter;
	public string warpObject;

	Vector3 pushDir;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		OrangePortal = GameObject.Find ("Orange Portal(Clone)");
	}
		

	void OnTriggerEnter(Collider other){
		warpObject = other.tag;
		other.transform.position = OrangePortal.transform.position;
	}

	public void PortalDirectionNESW(int direction){
		switch (direction) {
		case 0:
			pushDir = Vector3.up;
			break;
		case 1:
			pushDir = Vector3.right;
			break;
		case 2:
			pushDir = Vector3.down;
			break;
		case 3:
			pushDir = Vector3.left;
			break;
		}
	}
}
