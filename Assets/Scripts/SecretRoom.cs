using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretRoom : MonoBehaviour {
	public Text message;
	Player playerScript;
	bool displayed = false;
	public string text = "Join wolverinesoft";
	// Use this for initialization
	void Start () {
		playerScript = GameObject.FindWithTag ("Player").GetComponent<Player> ();
	}
	
	void OnTriggerEnter(){
		if (!displayed){
			StartCoroutine (displayText ());
		}
	}

	IEnumerator displayText(){
		displayed = true;
		GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().velocity = Vector3.zero;
		playerScript.movement = false;
		for (int i = 0; i < text.Length; i++){
			message.text += text [i];
			yield return new WaitForSeconds (0.2f);
		}
		playerScript.movement = true;
	}
}
