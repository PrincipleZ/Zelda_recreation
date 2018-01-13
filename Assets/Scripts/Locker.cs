using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {
	public GameObject entrancePrefab;
	public Sprite s;
	Inventory inventory;
	// Use this for initialization
	void Start () {
		inventory = GameObject.Find ("Player").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "Player"){
			if (inventory.key_count > 0) {
				string objectName = gameObject.name;
				if (objectName == "Tile_K_UPR" || objectName == "Tile_K_UPL")
					inventory.key_count -= 0.5f;
				else
					inventory.key_count -= 1f;
				GameObject temp = Instantiate (entrancePrefab, transform.position, Quaternion.identity);
				temp.GetComponent<SpriteRenderer> ().sprite = s;
				gameObject.SetActive (false);
			}
		}
	}
}
