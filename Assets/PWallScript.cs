using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWallScript : MonoBehaviour {

	public GameObject bluePortal;
	public GameObject orangePortal;

	GameObject temp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPortal(string color, int dir){
		if (color == "blue") {
			temp = Instantiate (bluePortal, transform.position, Quaternion.identity);
		} 
		else {
			temp = Instantiate (orangePortal, transform.position, Quaternion.identity);
			temp.GetComponent<OrangePortalScript> ().PortalDirectionNESW (dir);
		}
		temp.transform.parent = gameObject.transform;
	}
}
