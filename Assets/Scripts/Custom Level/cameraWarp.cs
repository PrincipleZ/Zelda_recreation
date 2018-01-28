using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraWarp : MonoBehaviour {
	public Vector3 warpValue;
	GameObject mainCamera;

	void Start(){
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void Update(){
		Debug.Log (mainCamera.transform.position);
	}
	public void warpCamera(){
		Debug.Log (this.gameObject);

		Vector3 temp = mainCamera.transform.position + warpValue;
		mainCamera.transform.position = temp;
	}

}
