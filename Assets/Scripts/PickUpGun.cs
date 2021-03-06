﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpGun : MonoBehaviour {
	public GameObject offhand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<PortalGun> ().enabled = true;
			offhand.SetActive (true);
			Destroy (this.gameObject);
            other.GetComponent<playerSounds>().GotNewWeapon();
            GameObject temp = GameObject.Find("Orange Portal(Clone)");
            temp.transform.parent.GetComponent<BoxCollider>().enabled = true;
            Destroy(temp);
            temp = GameObject.Find("Blue Portal(Clone)");
            temp.transform.parent.GetComponent<BoxCollider>().enabled = true;
            Destroy(temp);
        }
	}
}
