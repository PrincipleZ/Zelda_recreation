using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePortalScript : MonoBehaviour {

	public GameObject OrangePortal;
	public Vector3 directionToEnter;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	void OnTriggerEnter(Collider other){
		if (other.tag != "PWall") {
			other.transform.position = OrangePortal.transform.position;
		}
	}
}
