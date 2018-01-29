using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	public Vector3 cameraPosition = Vector3.zero;
	CheckPointSaver checkPointScript;
	// Use this for initialization
	void Start () {
		checkPointScript = GameObject.FindWithTag("Respawn").GetComponent<CheckPointSaver> ();
	}
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player"){
			cameraPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform.position;
			checkPointScript.setCheckPoint (this);
		}
	}


}
