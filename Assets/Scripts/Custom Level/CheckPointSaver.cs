using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaver : MonoBehaviour {
	public static GameObject saver;
	public GameObject checkPoint;
	public Vector3 checkPointPos = Vector3.zero;
	public Vector3 cameraPos= Vector3.zero;

	void Awake(){
		DontDestroyOnLoad (this);
		if (saver == null)
			saver = this.gameObject;
		else
			Destroy (this.gameObject);
	}

	public void setCheckPoint(CheckPoint checkPoint){
		checkPointPos = checkPoint.transform.position;
		cameraPos = checkPoint.cameraPosition;
	}
	public Vector3 getCheckPointPosition(){
		return checkPointPos;
	}

	public Vector3 getCameraPosition(){
		return cameraPos;
	}



	

}
