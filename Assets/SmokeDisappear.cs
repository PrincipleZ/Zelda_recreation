using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDisappear : MonoBehaviour {


	public void disappear(){
		StartCoroutine (countDown());
	}

	IEnumerator countDown(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

}
