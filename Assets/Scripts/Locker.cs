using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {
	public GameObject entrancePrefab;
	public Sprite s;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Player"){
			Debug.Log ("here");
			GameObject temp = Instantiate (entrancePrefab, transform.position, Quaternion.identity);
			temp.GetComponent<SpriteRenderer> ().sprite = s;
			gameObject.SetActive(false);
		}
	}
}
