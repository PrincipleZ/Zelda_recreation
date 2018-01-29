using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaver : MonoBehaviour {
	public static CheckPointSaver saver;
	public GameObject checkPoint;

	void Awake(){
		DontDestroyOnLoad (this);
		if (saver == null)
			saver = this;
		else
			Destroy (this);
	}
	 
	public GameObject getCheckPoint(){
		Debug.Log (checkPoint);

		return checkPoint;
	}
	

}
