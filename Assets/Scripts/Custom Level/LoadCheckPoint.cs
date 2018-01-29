using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCheckPoint : MonoBehaviour {
	public GameObject offhand;

	// Use this for initialization
	void Start () {
		CheckPointSaver checkPoint = GameObject.FindGameObjectWithTag ("Respawn").GetComponent<CheckPointSaver>();
		transform.position = checkPoint.getCheckPointPosition ();
		if (transform.position.y > 3){
			GetComponent<PortalGun> ().enabled = true;
			offhand.SetActive (true);
		}
		GameObject.FindWithTag ("MainCamera").transform.position = checkPoint.getCameraPosition ();
	}
	

}
