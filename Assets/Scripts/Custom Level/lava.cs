using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour {
	public GameObject smokePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Player> ().currentHealth = 0;
			GameObject tempSmoke = (GameObject)Instantiate (smokePrefab, transform.position, Quaternion.identity);
			tempSmoke.GetComponent<SmokeDisappear> ().disappear ();
		}
		else if (other.gameObject.tag == "enemy"){
			GameObject tempSmoke = (GameObject)Instantiate (smokePrefab, transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			tempSmoke.GetComponent<SmokeDisappear> ().disappear ();
		}
	}
}
